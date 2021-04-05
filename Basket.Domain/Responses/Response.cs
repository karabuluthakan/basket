using System;
using System.Net;
using System.Text.Json.Serialization;
using Basket.Domain.Responses.Abstract;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Response : IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        public Response(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public Response(bool success, string message) : this(success)
        {
            Message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="statusCode"></param>
        public Response(bool success, HttpStatusCode statusCode) : this(success)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public Response(bool success, HttpStatusCode statusCode, string message) : this(success, message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; }
    }
}