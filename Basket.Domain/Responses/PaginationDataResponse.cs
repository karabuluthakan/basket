using System;
using System.Collections.Generic;
using Basket.Domain.Responses.Abstract;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationDataResponse<T> : Response, IPaginationResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PaginationDataResponse(IReadOnlyList<T> data, bool success, int pageNumber, int pageSize) :
            base(success)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="message"></param>
        public PaginationDataResponse(IReadOnlyList<T> data, bool success, int pageNumber, int pageSize,
            string message) : base(success, message)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        /// <inheritdoc />
        public int PageNumber { get; set; }

        /// <inheritdoc />
        public int PageSize { get; set; }

        /// <inheritdoc />
        public Uri FirstPage { get; set; }

        /// <inheritdoc />
        public Uri LastPage { get; set; }

        /// <inheritdoc />
        public int TotalPages { get; set; }

        /// <inheritdoc />
        public int TotalRecords { get; set; }

        /// <inheritdoc />
        public Uri NextPage { get; set; }

        /// <inheritdoc />
        public Uri PreviousPage { get; set; }

        /// <inheritdoc />
        public IReadOnlyList<T> Data { get; }
    }
}