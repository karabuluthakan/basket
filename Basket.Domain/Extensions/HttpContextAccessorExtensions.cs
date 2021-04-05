using System;
using Microsoft.AspNetCore.Http;

namespace Basket.Domain.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpContextAccessorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetRequestUri(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            var request = httpContextAccessor.HttpContext.Request;
            return string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetRoute(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            return httpContextAccessor.HttpContext.Request.Path.Value;
        }
    }
}