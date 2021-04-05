using System.Threading.Tasks;

namespace Basket.Domain.SharedCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductStockProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ValueTask<int> GetStockForProduct(string productId);
    }
}