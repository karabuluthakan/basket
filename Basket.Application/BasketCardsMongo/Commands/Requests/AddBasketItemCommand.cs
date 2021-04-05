using Basket.Domain.Responses.Abstract;
using MediatR;

namespace Basket.Application.BasketCardsMongo.Commands.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class AddBasketItemCommand : IRequest<IResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public string BasketCardId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; } = 1;
    }
}