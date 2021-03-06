using System;
using System.Threading.Tasks;

namespace Basket.Domain.SharedCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheService : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ValueTask<T> GetAsync<T>(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Set(string key, object data, TimeSpan? expiry = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ValueTask<bool> IsExistsAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ValueTask<bool> RemoveAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        ValueTask RemoveByPatternAsync(string pattern);
    }
}