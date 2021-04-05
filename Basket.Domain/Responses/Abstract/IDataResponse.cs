using System.Text.Json.Serialization;

namespace Basket.Domain.Responses.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataResponse<out T> : IResponse where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("items")]
        T Items { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; }
    }
}