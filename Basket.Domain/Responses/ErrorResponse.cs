using System.Net;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorResponse : Response
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorResponse() : base(false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponse(string message) : base(false, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        public ErrorResponse(HttpStatusCode statusCode) : base(false, statusCode)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public ErrorResponse(HttpStatusCode statusCode, string message) : base(false, statusCode, message)
        {
        }
    }
}