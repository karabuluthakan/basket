using Basket.Domain.Entities.ValueObjects;
using Basket.Domain.Exceptions;
using MongoDB.Bson;
using Xunit;

namespace Basket.Test
{
    public class BasketItemTests
    {
        [Fact]
        public void NullProductCheck()
        {
            Assert.Throws<DomainException>(() => { new BasketItem(null, 1); });
        }

        [Fact]
        public void NegativeQuantityCheck()
        {
            var lookup = new LookupIdName(ObjectId.GenerateNewId().ToString(), "Test 1");
            var price = new Price("TRY", 98.99);
            var product = new ProductInfo(lookup, price);
            Assert.Throws<DomainException>(() => { new BasketItem(product, -1); });
        }

        [Fact]
        public void BasketItem()
        {
            // Arrange
            var objectId = ObjectId.GenerateNewId().ToString();
            var lookup = new LookupIdName(objectId, "Test 1");
            var price = new Price("TRY", 98.99);
            var product = new ProductInfo(lookup, price);
            
            // Act
            var basketItem = new BasketItem(product, 1);

            // Assert
            Assert.Equal("Test 1", basketItem.Product.Product.Name);
            Assert.Equal("TRY", basketItem.Product.Price.Currency);
            Assert.NotNull(basketItem.Product.Product.Id);
            Assert.Equal(objectId,basketItem.Product.Product.Id);
        }
    }
}