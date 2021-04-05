using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Basket.Application.BasketCardsMongo.Commands.Requests;
using Basket.Domain.Entities.ValueObjects;
using Basket.Domain.Localized;
using Basket.Domain.Responses;
using Basket.Domain.Responses.Abstract;
using Basket.Domain.SharedCore;
using Basket.Infrastructure.DataAccess.Abstract;
using MediatR;

namespace Basket.Application.BasketCardsMongo.Commands.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommand, IResponse>
    {
        private readonly IBasketCardRepository _basketCardRepository;
        private readonly IResponseLocalized _localized;
        private readonly IProductStockProvider _productStock;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basketCardRepository"></param>
        /// <param name="localized"></param>
        /// <param name="productStock"></param>
        public AddBasketItemCommandHandler(IBasketCardRepository basketCardRepository, IResponseLocalized localized,
            IProductStockProvider productStock)
        {
            _basketCardRepository =
                basketCardRepository ?? throw new ArgumentNullException(nameof(basketCardRepository));
            _localized = localized ?? throw new ArgumentNullException(nameof(localized));
            _productStock = productStock ?? throw new ArgumentNullException(nameof(productStock));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResponse> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var card = await _basketCardRepository.GetByIdAsync(request.BasketCardId, cancellationToken);
                var stock = await _productStock.GetStockForProduct(request.ProductId);
                var product = new ProductInfo(new LookupIdName(request.ProductId, request.ProductName),
                    new Price("TRY", request.Amount));
                var item = new BasketItem(product, request.Quantity);
                card.AddItem(item, stock);
                return await _basketCardRepository.UpdateAsync(card.Id, card, cancellationToken);
            }
            catch
            {
                return new ErrorResponse(HttpStatusCode.BadRequest, _localized.GetString("NotAdded"));
            }
        }
    }
}