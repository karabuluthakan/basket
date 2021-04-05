using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.BasketCardsRedis.Commands.Requests;
using Basket.Application.Dtos;
using Basket.Domain.AppSettings;
using Basket.Domain.Localized;
using Basket.Domain.Responses;
using Basket.Domain.Responses.Abstract;
using Basket.Domain.SharedCore;
using MediatR;
using Microsoft.Extensions.Options;

namespace Basket.Application.BasketCardsRedis.Commands.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisDeleteBasketItemCommandHandler : IRequestHandler<RedisDeleteBasketItemCommand, IResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly IResponseLocalized _localized;
        private readonly BasketRulesSettings _basketRules;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheService"></param>
        /// <param name="localized"></param>
        /// <param name="basketRules"></param>
        public RedisDeleteBasketItemCommandHandler(ICacheService cacheService, IResponseLocalized localized,
            IOptions<BasketRulesSettings> basketRules)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _localized = localized ?? throw new ArgumentNullException(nameof(localized));
            _basketRules = basketRules.Value ?? throw new ArgumentNullException(nameof(basketRules.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> 
        public async Task<IResponse> Handle(RedisDeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var exist = await _cacheService.IsExistsAsync(request.CustomerId);
            if (!exist)
            {
                return new ErrorResponse(HttpStatusCode.NotFound, _localized.GetString("BasketNotFound"));
            }

            // Sepetin tamamını değil de belirli ürünleri silmek istediğinde Client productId gönderir ve buna göre silme işlemi yaparız diye düşündüm!
            if (!string.IsNullOrEmpty(request.ProductId))
            {
                var basketExpiryDate = TimeSpan.FromDays(_basketRules.ExpirationDurationInDays);
                var basketItems = await _cacheService.GetAsync<List<BasketItemDto>>(request.CustomerId);
                basketItems.RemoveAll(x => x.Product.Product.Id.Equals(request.ProductId));
                var set = _cacheService.Set(request.CustomerId, basketItems, basketExpiryDate);
                if (set)
                {
                    return new SuccessResponse(HttpStatusCode.OK, _localized.GetString("SuccessfullyDeleted"));
                }

                return new ErrorResponse(HttpStatusCode.BadRequest, _localized.GetString("NotDeleted"));
            }

            var remove = await _cacheService.RemoveAsync(request.CustomerId);
            if (remove)
            {
                return new SuccessResponse(HttpStatusCode.OK, _localized.GetString("SuccessfullyDeleted"));
            }

            return new ErrorResponse(HttpStatusCode.BadRequest, _localized.GetString("NotDeleted"));
        }
    }
}