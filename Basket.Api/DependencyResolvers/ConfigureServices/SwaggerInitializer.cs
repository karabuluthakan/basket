using System;
using Basket.Domain.IoC;
using Basket.Domain.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Basket.Api.DependencyResolvers.ConfigureServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerInitializer : IConfigureServiceModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Load(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(DefaultConstants.DefaultCorsPolicy,
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(86400))
                            .WithExposedHeaders("WWW-Authenticate"));
            });


            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("BasketApi",new OpenApiInfo
                {
                    Title = "Basket Api",
                    Version = "1.0",
                    Description = "Basket Api Challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "Hakan Karabulut",
                        Url = new Uri("https://github.com/karabuluthakan"),
                        Email = "34hk1286@gmail.com"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms")
                });
            });
        }
    }
}