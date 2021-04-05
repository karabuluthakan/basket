using Microsoft.Extensions.DependencyInjection;

namespace Basket.Domain.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigureServiceModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        void Load(IServiceCollection services);
    }
}