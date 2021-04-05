using Basket.Application.Extensions;
using Basket.Domain.IoC;
using Basket.Domain.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Basket.Api.DependencyResolvers.Configure
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultConfigure : IConfigureModule
    {
        /// <summary>
        /// 
        /// </summary>
        public int Priority { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Load(IApplicationBuilder app)
        {
            ServiceTool.ServiceProvider = app.ApplicationServices;

            app.UseCors(DefaultConstants.DefaultCorsPolicy)
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                })
                .UseRouting()
                .UseSwagger()
                .UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/BasketApi/swagger.json","Basket Api");
                })
                .UseRequestLocalized()
                .UseEndpoints(builder => { builder.MapControllers(); });
        }
    }
}