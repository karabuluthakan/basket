using System.Net;
using Basket.Domain.Responses.Abstract;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class SuccessResponse : Response, IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public SuccessResponse() : base(true)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SuccessResponse(string message) : base(true, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        public SuccessResponse(HttpStatusCode statusCode) : base(true, statusCode)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public SuccessResponse(HttpStatusCode statusCode, string message) : base(true, statusCode, message)
        {
        }
    }
}