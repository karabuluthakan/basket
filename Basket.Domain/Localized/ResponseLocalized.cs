using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Basket.Domain.AppSettings;
using Basket.Domain.Extensions.Structures;
using Microsoft.Extensions.Options; 
using Microsoft.AspNetCore.Hosting;

namespace Basket.Domain.Localized
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseLocalized : IResponseLocalized
    {
        private readonly LocalizedJsonLookup _localizedLookup;
        private readonly string _addingResource;
        private readonly CultureInfo _cultureInfo;
        private readonly string _contentRootPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="options"></param>
        public ResponseLocalized(IWebHostEnvironment environment, IOptions<LocalizedSettings> options)
        {
            _contentRootPath = environment.ContentRootPath ?? throw new ArgumentNullException(nameof(environment));
            _cultureInfo = Thread.CurrentThread.CurrentCulture;
            _addingResource = options?.Value?.ResourceName;
            _localizedLookup = new LocalizedJsonLookup();
            BootstrapLoading();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void AddResource(string path)
        {
            var serviceLookups = GetJsonFile(path);
            _localizedLookup.Items.AddRange(serviceLookups.Items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentCultureName()
        {
            return GetCurrentCulture();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            var value = _localizedLookup.Items.FirstOrDefault(x => x.Key.Equals(key));
            if (value is null)
            {
                return string.Empty;
            }

            var valueKey = GetCurrentCulture();
            return value.Value[valueKey];
        }

        private void BootstrapLoading()
        {
            if (!string.IsNullOrEmpty(_addingResource))
            {
                AddResource(_addingResource);
            }
        }

        private LocalizedJsonLookup GetJsonFile(string resource)
        {
            try
            {
                var path = GetPath(resource);
                var json = File.ReadAllText(path, Encoding.UTF8);
                var localizedLookup = json.FromJson<LocalizedJsonLookup>();
                return localizedLookup;
            }
            catch
            {
                return new LocalizedJsonLookup();
            }
        }

        private string GetPath(string resource)
        {
            if (File.Exists(GetProductPath(resource)))
            {
                return GetProductPath(resource);
            }

            return File.Exists(GetDebugPath(resource)) ? GetDebugPath(resource) : string.Empty;
        }

        private string GetProductPath(string resource)
        {
            return Path.Combine(_contentRootPath, LocalizedConstants.ResourcePath, resource);
        }

        private string GetDebugPath(string resource)
        {
            return Path.Combine(_contentRootPath, LocalizedConstants.ExecuteFolder, LocalizedConstants.Debug,
                LocalizedConstants.ApplicationVersion, LocalizedConstants.ResourcePath,
                resource);
        }

        private string GetCurrentCulture()
        {
            var cultureKey = _cultureInfo.Name.Split("-")[0].ToLower();
            return string.IsNullOrEmpty(cultureKey) ? LocalizedConstants.DefaultCulture : cultureKey;
        }
    }
}