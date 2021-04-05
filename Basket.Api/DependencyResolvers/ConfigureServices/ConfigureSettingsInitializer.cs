using Basket.Domain.AppSettings;
using Basket.Domain.IoC;
using Basket.Domain.Localized;
using Basket.Infrastructure.Caching.Redis;
using Basket.Infrastructure.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;

namespace Basket.Api.DependencyResolvers.ConfigureServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureSettingsInitializer : IConfigureServiceModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Load(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.Configure<RedisCacheSettings>(
                options => configuration.GetSection(nameof(RedisCacheSettings)).Bind(options));

            services.Configure<BasketRulesSettings>(
                options => configuration.GetSection(nameof(BasketRulesSettings)).Bind(options));

            services.Configure<LocalizedSettings>(options =>
                options.ResourceName = LocalizedConstants.LocalizedResourcePath);

            services.Configure<MongoDbSettings>(options =>
                configuration.GetSection(nameof(MongoDbSettings)).Bind(options));

            services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.TryAddSingleton(sp =>
            {
                var provider = sp.GetRequiredService<ObjectPoolProvider>();
                var policy = new StringBuilderPooledObjectPolicy();
                return provider.Create(policy);
            });
        }
    }
}