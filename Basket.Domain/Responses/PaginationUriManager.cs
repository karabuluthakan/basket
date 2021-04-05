using System;
using Basket.Domain.Extensions;
using Basket.Domain.Responses.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationUriManager : IPaginationUriService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public PaginationUriManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <returns></returns>
        public Uri GetPageUri(PaginationQuery paginationQuery)
        {
            var baseUri = httpContextAccessor.GetRequestUri();
            var route = httpContextAccessor.GetRoute();
            var endpoint = new Uri(string.Concat(baseUri, route));
            var queryUri = QueryHelpers.AddQueryString($"{endpoint}", "pageNumber", $"{paginationQuery.PageNumber}");
            queryUri = QueryHelpers.AddQueryString(queryUri, "pageSize", $"{paginationQuery.PageSize}");
            return new Uri(queryUri);
        }
    }
}