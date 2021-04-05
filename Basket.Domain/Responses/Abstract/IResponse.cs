using System.Net;
using System.Text.Json.Serialization;

namespace Basket.Domain.Responses.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("success")]
        bool Success { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("message")]
        string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; }
    }
}