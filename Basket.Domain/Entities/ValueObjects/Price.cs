using System;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Exceptions;
using Basket.Domain.Utilities;

namespace Basket.Domain.Entities.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class Price : ValueObject<Price>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Currency { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public double Amount { get; private set; }

        private Price()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <exception cref="DomainException"></exception>
        public Price(string currency, double amount)
        {
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != DefaultConstants.CURRENCY_LENGTH)
            {
                throw new DomainException($"{nameof(currency)} expected to be 3 chars!",
                    new ArgumentException(nameof(currency)));
            }

            Currency = currency;
            if (!(amount > 0))
            {
                throw new DomainException($"{nameof(amount)} should be positive!",
                    new InvalidOperationException(nameof(amount)));
            }

            Amount = amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object[] PropertiesToCheckForEquality() => new object[] {Currency, Amount};
    }
}