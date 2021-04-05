using Basket.Domain.Entities.Abstract;

namespace Basket.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductDto : IDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}