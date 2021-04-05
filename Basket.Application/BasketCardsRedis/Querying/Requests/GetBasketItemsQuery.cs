using Basket.Domain.Responses.Abstract;
using MediatR;

namespace Basket.Application.BasketCardsRedis.Querying.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class GetBasketItemsQuery : IRequest<IResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public string CustomerId { get; set; }
    }
}