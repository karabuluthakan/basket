using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RedisAddBasketItemCommandHandler : IRequestHandler<RedisAddBasketItemCommand, IResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly BasketRulesSettings _basketRules;
        private readonly IProductStockProvider _stockProvider;
        private readonly IResponseLocalized _localized;
        private readonly IMapperAdapter _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheService"></param>
        /// <param name="basketRules"></param>
        /// <param name="stockProvider"></param>
        /// <param name="localized"></param>
        /// <param name="mapper"></param>
        public RedisAddBasketItemCommandHandler(ICacheService cacheService, IOptions<BasketRulesSettings> basketRules,
            IProductStockProvider stockProvider, IResponseLocalized localized, IMapperAdapter mapper)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _stockProvider = stockProvider ?? throw new ArgumentNullException(nameof(stockProvider));
            _localized = localized ?? throw new ArgumentNullException(nameof(localized));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _basketRules = basketRules.Value ?? throw new ArgumentNullException(nameof(basketRules.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> 
        public async Task<IResponse> Handle(RedisAddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var productId = request.Product.Product.Id;
            var stock = await _stockProvider.GetStockForProduct(productId);
            var quantity = request.Quantity;
            if (quantity > stock)
            {
                return new ErrorResponse(HttpStatusCode.BadRequest,
                    _localized.GetString("CannotGreaterThanTheStock"));
            }

            var basketExpiryDate = TimeSpan.FromDays(_basketRules.ExpirationDurationInDays);
            var basketItems = await _cacheService.GetAsync<List<BasketItemDto>>(request.CustomerId);
            if (basketItems != null && basketItems.Any())
            {
                var basketQuantity =
                    basketItems.Where(x => x.Product.Product.Id.Equals(productId))
                        .Sum(x => x.Quantity) + quantity;

                if (basketQuantity > stock)
                {
                    return new ErrorResponse(HttpStatusCode.BadRequest,
                        _localized.GetString("CannotGreaterThanTheStock"));
                }

                var existItem = basketItems.SingleOrDefault(x => x.Product.Product.Id.Equals(productId));
                if (existItem != null)
                {
                    existItem.Quantity += quantity;
                }
                else
                {
                    var productInfo = _mapper.Map<ProductInfoDto>(request.Product);
                    var item = new BasketItemDto
                    {
                        Product = productInfo,
                        Quantity = quantity
                    };
                    basketItems.Add(item);
                }

                var set = _cacheService.Set(request.CustomerId, basketItems, basketExpiryDate);
                if (set)
                {
                    return new SuccessResponse(HttpStatusCode.OK, _localized.GetString("SuccessfullyAdded"));
                }

                return new ErrorResponse(HttpStatusCode.BadRequest, _localized.GetString("NotAdded"));
            }

            var basketItem = _mapper.Map<BasketItemDto>(request.Product);
            basketItem.Quantity = quantity;
            var setItem = _cacheService.Set(request.CustomerId, basketItem, basketExpiryDate);
            if (setItem)
            {
                return new SuccessResponse(HttpStatusCode.OK, _localized.GetString("SuccessfullyAdded"));
            }

            return new ErrorResponse(HttpStatusCode.BadRequest, _localized.GetString("NotAdded"));
        }
    }
}