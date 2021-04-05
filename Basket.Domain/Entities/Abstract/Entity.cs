using System;

namespace Basket.Domain.Entities.Abstract
{
    /// <summary>
    /// 
    /// </summary> 
    public abstract class Entity : IEntity<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Orm araçları için boş constructor gerekmektedir!
        /// </summary>
        protected Entity()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected Entity(string id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                throw new ArgumentNullException(nameof(id));
            }

            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Entity other) => Id.Equals(other.Id);

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

            return Equals((Entity) obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        public void SetCreatedAt()
        {
            CreatedAt = DateTime.Now;
        }
    }
}