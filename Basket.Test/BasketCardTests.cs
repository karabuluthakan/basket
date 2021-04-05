using Basket.Domain.Entities;
using Basket.Domain.Exceptions;
using Xunit;

namespace Basket.Test
{
    public class BasketCardTests
    {
        [Fact]
        public void AddNullBasketItemThrowsDomainException()
        {
            var card = new BasketCard();
            Assert.Throws<DomainException>(() => { card.AddItem(null, 1); });
        }

        
    }
}