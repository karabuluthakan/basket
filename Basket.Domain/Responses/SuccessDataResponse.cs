using System.Net;

namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessDataResponse<T> : DataResponse<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public SuccessDataResponse(T data) : base(data, true, "success")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public SuccessDataResponse(T data, string message) : base(data, true, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param> 
        public SuccessDataResponse(T data, int size) : base(data, true, HttpStatusCode.OK, size)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        public SuccessDataResponse(T data, HttpStatusCode statusCode) : base(data, true, statusCode)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <param name="size"></param>
        public SuccessDataResponse(T data, HttpStatusCode statusCode, int size) : base(data, true, statusCode, size)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        public SuccessDataResponse(T data, HttpStatusCode statusCode, int size, int offset, int limit) : base(data,
            true, statusCode, size)
        {
            this.Offset = offset;
            this.Limit = limit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public SuccessDataResponse(T data, HttpStatusCode statusCode, string message) : base(data, true, statusCode,
            message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int Offset { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int Limit { get; private set; } = 1;
    }
}