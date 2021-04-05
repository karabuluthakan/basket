using Basket.Application.Dtos;
using Basket.Domain.Responses.Abstract;
using MediatR;

namespace Basket.Application.BasketCardsRedis.Commands.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisAddBasketItemCommand : BasketItemDto, IRequest<IResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public string CustomerId { get; set; }
    }
}