using Basket.Domain.Entities.Abstract;

namespace Basket.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class PriceDto : IDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Amount { get; set; }
    }
}