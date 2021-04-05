using Basket.Domain.Entities.Abstract;

namespace Basket.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductInfoDto : IDto
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductDto Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PriceDto Price { get; set; }
    }
}