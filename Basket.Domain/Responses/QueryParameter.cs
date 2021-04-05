using Basket.Domain.Utilities;

namespace Basket.Domain.Responses
{
    /// <summary>
    ///     QueryParameter class
    /// </summary>
    public class QueryParameter
    {
        /// <summary>
        /// Maximum number of items to return
        /// </summary>
        public int _limit { get; set; } = DefaultConstants.DefaultItemCount;

        /// <summary>
        /// Number of items to be skipped
        /// </summary>
        public int _offset { get; set; } = DefaultConstants.DefaultStartOffset;

        /// <summary>
        /// Order by statement
        /// </summary>
        /// 
        public string _orderBy { get; set; } = null;

        /// <summary>
        /// Filter to be applied to the query
        /// </summary>
        public string _filter { get; set; } = null;
    }
}