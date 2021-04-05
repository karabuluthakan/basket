using System;

namespace Basket.Domain.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}