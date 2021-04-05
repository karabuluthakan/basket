using System.Linq;

namespace Basket.Domain.Entities.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValueObject
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueObject<T> : IValueObject where T : ValueObject<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract object[] PropertiesToCheckForEquality();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(ValueObject<T> other) =>
            PropertiesToCheckForEquality().SequenceEqual(other.PropertiesToCheckForEquality());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((ValueObject<T>) obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            PropertiesToCheckForEquality()
                .Aggregate(7, (current, prop) => current * (prop.GetHashCode() + 13));
    }
}