using System;

namespace Basket.Domain.Responses.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPaginationUriService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <returns></returns>
        public Uri GetPageUri(PaginationQuery paginationQuery);
    }
}