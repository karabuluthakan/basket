using System;
using System.Collections.Generic;
using System.Linq;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Entities.ValueObjects;
using Basket.Domain.Exceptions;

namespace Basket.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BasketCard : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public BasketCard()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basketItem"></param>
        /// <param name="stock"></param>
        /// <exception cref="DomainException"></exception>
        public void AddItem(BasketItem basketItem, int stock)
        {
            if (basketItem is null)
            {
                throw new DomainException($"{nameof(basketItem)} cannot be null!",
                    new ArgumentNullException(nameof(basketItem)));
            }

            BasketItem existItem = null;
            if (BasketItems?.Count > 0)
            {
                existItem =
                    BasketItems.SingleOrDefault(x => x.Product.Product.Id.Equals(basketItem.Product.Product.Id));
            }

            ValidateStock(existItem, basketItem, stock);
            if (existItem is null)
            {
                BasketItems.Add(basketItem);
            }
            else
            {
                existItem.Increase(basketItem.Quantity);
            }
        }

        private void ValidateStock(BasketItem existItem, BasketItem newItem, int stock)
        {
            if (stock <= 0)
            {
                throw new DomainException("Insufficient stock!", new InvalidOperationException());
            }

            var target = (existItem?.Quantity ?? 0) + newItem.Quantity;
            if (target > stock)
            {
                throw new DomainException("Quantity cannot add greater than stock!", new InvalidOperationException());
            }
        }
    }
}