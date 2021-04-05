using System;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Exceptions;

namespace Basket.Domain.Entities.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class BasketItem : ValueObject<BasketItem>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductInfo Product { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> 
        protected override object[] PropertiesToCheckForEquality()
        {
            throw new System.NotImplementedException();
        }

        private BasketItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <exception cref="DomainException"></exception>
        public BasketItem(ProductInfo product, int quantity)
        {
            Product = product ?? throw new DomainException($"{nameof(product)} cannot be nul!",
                new ArgumentNullException(nameof(product)));

            if (quantity < 0)
            {
                throw new DomainException($"{nameof(quantity)} greater than 0",
                    new ArgumentException(nameof(quantity)));
            }
            
        }

        internal void Increase(uint quantityCount)
        {
            Quantity += quantityCount;
        }
    }
}