namespace Basket.Domain.Responses
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorDataResponse<T> : DataResponse<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public ErrorDataResponse(T data) : base(data, false,"error")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public ErrorDataResponse(T data, string message) : base(data, false, message)
        {
        }
    }
}