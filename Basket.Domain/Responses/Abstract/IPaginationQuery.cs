namespace Basket.Domain.Responses.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPaginationQuery
    {
        /// <summary>
        /// 
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int PageSize { get; set; }
    }
}