using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Traning.AspNetCore.Microservices.Catalog.Application.CQRS;
using Traning.AspNetCore.Microservices.Catalog.Application.Mapping;

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

            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddMediatR(typeof(ProductsViewQueryHandler).GetTypeInfo().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMicroserviceExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("v1/swagger.json", "Catalog");
            });
        }
    }
}
