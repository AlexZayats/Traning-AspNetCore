using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Traning.AspNetCore.Microservices.Catalog.Application;
using Traning.AspNetCore.Microservices.Catalog.Application.CQRS;
using Traning.AspNetCore.Microservices.Catalog.Application.Mapping;
using Traning.AspNetCore.Microservices.Catalog.Data;

namespace Traning.AspNetCore.Microservices.Catalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ICatalogDbContext, CatalogDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["DATABASE"]);
#if DEBUG
                    options.EnableSensitiveDataLogging();
#endif
                });
            services
                .AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                options.Authority += "/v2.0";
                options.TokenValidationParameters.ValidAudiences = new[] { options.Audience, $"api://{options.Audience}" };
                options.TokenValidationParameters.ValidateIssuer = false;
            });
            services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductCreateCommandValidator>());
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
                    var assembly = Assembly.GetExecutingAssembly();
                    var apiXmlPath = Path.ChangeExtension(assembly.Location, "xml");
                    options.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
                    options.AddSecurityDefinition("aad-jwt", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{Configuration["AzureAd:Instance"] + Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                                TokenUrl = new Uri($"{Configuration["AzureAd:Instance"] + Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
                                Scopes = new Dictionary<string, string>
                                {
                                    { "openid", "Sign In Permissions" },
                                    { "profile", "User Profile Permissions" },
                                    { $"api://{Configuration["AzureAd:ClientId"]}/user_impersonation", "Application API Permissions" }
                                }
                            }
                        }
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "aad-jwt" }
                            },
                            new string[0]
                        }
                    });
                });
            services.AddSerilogLogging();
            services.AddOpenTracing();
            services.AddJaeger();
            services.AddPipelineBehavior();

            services
                .AddHealthChecks()
                .AddDatabaseCheck<ICatalogDbContext, CatalogDbContext>(tags: new[] { "ready" });

            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddMediatR(typeof(ProductsViewQueryHandler).GetTypeInfo().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMicroserviceExceptionHandler();
            app.UseMicroserviceHealthChecks();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger(options => options.PreSerializeFilters.Add((swagger, request) =>
            {
                swagger.Servers.Add(new OpenApiServer { Url = $"{request.Scheme}://{request.Host.Value}{Configuration.GetValue("BASE_URL", string.Empty)}" });
            }));
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(Configuration["AzureAd:ClientId"]);
                options.SwaggerEndpoint("v1/swagger.json", "Catalog");
            });
        }
    }
}
