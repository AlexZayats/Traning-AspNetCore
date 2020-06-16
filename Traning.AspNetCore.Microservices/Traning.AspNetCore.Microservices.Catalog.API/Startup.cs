using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
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
                .AddDbContext<ICatalogDbContext, CatalogDbContext>(options => options.UseSqlServer(Configuration["DATABASE"]));
            services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductCreateCommandValidator>()); ;
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
                    var assembly = Assembly.GetExecutingAssembly();
                    var apiXmlPath = Path.ChangeExtension(assembly.Location, "xml");
                    options.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
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
                options.SwaggerEndpoint("v1/swagger.json", "Catalog");
            });
        }
    }
}
