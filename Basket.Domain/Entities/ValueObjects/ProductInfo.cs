using System;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Exceptions;

namespace Basket.Domain.Entities.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductInfo : ValueObject<ProductInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        public LookupIdName Product { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Price Price { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> 
        protected override object[] PropertiesToCheckForEquality()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="price"></param>
        public ProductInfo(LookupIdName product, Price price)
        {
            Product = product ?? throw new DomainException($"{nameof(product)} not null", new ArgumentNullException(nameof(product)));
            Price = price ?? throw new DomainException($"{nameof(price)} not null", new ArgumentNullException(nameof(price)));;
        }
    }
}