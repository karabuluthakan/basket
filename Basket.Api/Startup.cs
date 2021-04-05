using System;
using System.Linq;
using Basket.Domain.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Resolvers IConfigureServiceModule installer

            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IConfigureServiceModule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IConfigureServiceModule>().ToList();
            installers.ForEach(installer => installer.Load(services));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param> 
        public void Configure(IApplicationBuilder app)
        {
            #region Dependency Resolvers IConfigureModule installer

            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IConfigureModule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IConfigureModule>().OrderBy(x => x.Priority).ToList();
            installers.ForEach(installer => installer.Load(app));

            #endregion
        }
    }
}