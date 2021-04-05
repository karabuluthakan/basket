using System;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Exceptions;

namespace Basket.Domain.Entities.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class LookupIdName : ValueObject<LookupIdName>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object[] PropertiesToCheckForEquality() => new object[] {Id, Name};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <exception cref="DomainException"></exception>
        public LookupIdName(string id, string name)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new DomainException($"{nameof(id)} cannot be null", new ArgumentNullException(nameof(id)));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException($"{nameof(name)} cannot be null", new ArgumentNullException(nameof(name)));
            }

            Id = id;
            Name = name;
        }
    }
}