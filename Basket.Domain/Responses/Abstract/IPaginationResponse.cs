using System;
using System.Collections.Generic;

namespace Basket.Domain.Responses.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPaginationResponse<out T> : IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        IReadOnlyList<T> Data { get; }
        /// <summary>
        /// 
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Uri FirstPage { get; }
        /// <summary>
        /// 
        /// </summary>
        Uri LastPage { get; }
        /// <summary>
        /// 
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 
        /// </summary>
        int TotalRecords { get; }
        /// <summary>
        /// 
        /// </summary>
        Uri NextPage { get; }
        /// <summary>
        /// 
        /// </summary>
        Uri PreviousPage { get; }
    }
}