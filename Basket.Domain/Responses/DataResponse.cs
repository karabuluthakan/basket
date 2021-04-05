using System;
using System.Net;
using System.Text.Json.Serialization;
using Basket.Domain.Responses.Abstract;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class DataResponse<T> : Response, IDataResponse<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        public DataResponse(T data, bool success) : base(success)
        {
            Items = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="size"></param>
        public DataResponse(T data, bool success, int size) : this(data, success)
        {
            Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="statusCode"></param>
        public DataResponse(T data, bool success, HttpStatusCode statusCode) : base(success, statusCode)
        {
            Items = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="statusCode"></param>
        /// <param name="size"></param>
        public DataResponse(T data, bool success, HttpStatusCode statusCode, int size) : this(data, success, statusCode)
        {
            Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public DataResponse(T data, bool success, string message) : base(success, message)
        {
            Items = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="size"></param>
        public DataResponse(T data, bool success, string message, int size) : this(data, success, message)
        {
            Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public DataResponse(T data, bool success, HttpStatusCode statusCode, string message) : base(success, statusCode,
            message)
        {
            Items = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="success"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="size"></param>
        public DataResponse(T data, bool success, HttpStatusCode statusCode, string message, int size) : this(data,
            success, statusCode,
            message)
        {
            Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("items")]
        public T Items { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; } = 1;
    }
}