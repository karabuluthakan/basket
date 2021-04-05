using Basket.Domain.Responses.Abstract;
using Basket.Domain.Utilities;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationQuery : IPaginationQuery
    {
        private const int MinimumPageNumber = 1;
        /// <summary>
        /// 
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PaginationQuery()
        {
            this.PageNumber = MinimumPageNumber;
            this.PageSize = DefaultConstants.DefaultItemCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PaginationQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < MinimumPageNumber ? MinimumPageNumber : pageNumber;
            this.PageSize = pageSize < DefaultConstants.DefaultItemCount ? DefaultConstants.DefaultItemCount : pageSize;
        }
    }
}