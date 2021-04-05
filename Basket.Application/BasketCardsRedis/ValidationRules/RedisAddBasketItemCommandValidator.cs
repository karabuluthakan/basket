using Basket.Application.BasketCardsRedis.Commands.Requests;
using FluentValidation;
using MongoDB.Bson;

namespace Basket.Application.BasketCardsRedis.ValidationRules
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisAddBasketItemCommandValidator : AbstractValidator<RedisAddBasketItemCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        public RedisAddBasketItemCommandValidator()
        {
            RuleFor(x => x.Product.Product.Id).NotNull().NotEmpty()
                .Must((product, id) => CheckObjectId(id).Equals(true));

            RuleFor(x => x.Product.Product.Name).NotEmpty().NotNull();

            RuleFor(x => x.Quantity).GreaterThan(0);

            RuleFor(x => x.Product.Price.Currency).NotEmpty().NotNull().Length(3);

            RuleFor(x => x.Product.Price.Amount).NotEmpty().NotNull().GreaterThan(0);
        }

        private bool CheckObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}