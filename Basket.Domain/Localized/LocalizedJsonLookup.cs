using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Basket.Domain.Localized
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizedJsonLookup
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName(LocalizedConstants.Items)]
        public List<LocalizedJsonItems> Items { get; set; } = new List<LocalizedJsonItems>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class LocalizedJsonItems
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName(LocalizedConstants.Key)]
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName(LocalizedConstants.Value)]
        public Dictionary<string, string> Value { get; set; }
    }
}