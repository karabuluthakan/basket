using Basket.Application.BasketCardsMongo.Commands.Requests;
using FluentValidation;
using MongoDB.Bson;

namespace Basket.Application.BasketCardsRedis.ValidationRules
{
    /// <summary>
    /// 
    /// </summary>
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
            RuleFor(x => x.ProductId).NotEmpty().NotNull()
                .Must((product, id) => CheckObjectId(id).Equals(true));

            RuleFor(x => x.BasketCardId).NotEmpty().NotNull()
                .Must((product, id) => CheckObjectId(id).Equals(true));
        }

        private bool CheckObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}