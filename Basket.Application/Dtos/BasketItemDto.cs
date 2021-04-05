using Basket.Domain.Entities.Abstract;

namespace Basket.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class BasketItemDto : IDto
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductInfoDto Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
    }
}