using Basket.Domain.Responses.Abstract;
using MediatR;

namespace Basket.Application.BasketCardsRedis.Commands.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisDeleteBasketItemCommand : IRequest<IResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }
    }
}