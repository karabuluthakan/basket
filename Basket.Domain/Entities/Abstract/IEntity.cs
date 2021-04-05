using System;

namespace Basket.Domain.Entities.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEntity where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; }
    }
}