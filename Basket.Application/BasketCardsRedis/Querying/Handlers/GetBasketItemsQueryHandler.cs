using System;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.BasketCardsRedis.Querying.Requests;
using Basket.Domain.Responses.Abstract;
using Basket.Domain.SharedCore;
using MediatR;

namespace Basket.Application.BasketCardsRedis.Querying.Handlers
{
    /// <summary>
    /// 
    /// </summary> 
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQuery, IResponse>
    {
        private readonly ICacheService _cacheService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheService"></param>
        public GetBasketItemsQueryHandler(ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> 
        public async Task<IResponse> Handle(GetBasketItemsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}