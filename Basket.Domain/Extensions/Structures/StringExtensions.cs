using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Domain.Extensions.Structures
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public static TDest FromJson<TDest>(this string source)
        {
            return JsonSerializer.Deserialize<TDest>(source, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            });
        } 
    }
}