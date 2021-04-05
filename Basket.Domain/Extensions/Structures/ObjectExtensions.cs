using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Domain.Extensions.Structures
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public static TDest ShallowClone<TDest>(this object source) where TDest : new()
        {
            if (source == null)
            {
                return default;
            }

            var result = new TDest();
            var destProps = typeof(TDest).GetProperties();
            var props = source.GetType().GetProperties().Where(x => x.CanRead && x.CanWrite);
            foreach (var prop in props)
            {
                var destProp =
                    destProps.FirstOrDefault(t => t.Name == prop.Name && t.PropertyType == prop.PropertyType);
                if (destProp != null)
                {
                    destProp.SetValue(result, prop.GetValue(source));
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public static TDest DeepClone<TDest>(this object source)
        {
            return source == null
                ? default
                : source.AsJson().FromJson<TDest>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static TSource DeepClone<TSource>(this TSource source)
        {
            return source == null ? default : source.AsJson().FromJson<TSource>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string AsJson(this object source)
        {
            return JsonSerializer.Serialize(source, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            });
        }
    }
}