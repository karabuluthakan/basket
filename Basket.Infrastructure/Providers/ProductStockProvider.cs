using System;
using System.Threading.Tasks;
using Basket.Domain.SharedCore;

namespace Basket.Infrastructure.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductStockProvider : IProductStockProvider
    {
        /*private readonly HttpClient _httpClient;
        private readonly RetryPolicy<HttpResponseMessage> _httpRequestPolicy;


        public ProductStockProvider(IHttpClientFactory httpClientFactory)
        {
            // İstek başarısız olduğunda 5sn aralıklarla 3 kere request'i tekrar etmesi için kullandım.
            _httpRequestPolicy = Policy.HandleResult<HttpResponseMessage>
                    (x => !x.IsSuccessStatusCode)
                .WaitAndRetry(3, c => TimeSpan.FromSeconds(5));

            _httpClient = httpClientFactory.CreateClient("productStock");
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async ValueTask<int> GetStockForProduct(string productId)
        {
            var random = new Random(50);
            return await Task.FromResult(random.Next());

            // Ürün stok durumunu get method'lu stock isimli bir EP'den kontrol ettiğimi varsayıyorum.
            // var response =
            //     _httpRequestPolicy.Execute(() => _httpClient.GetAsync($"/products/{productId}/stock").Result);
            // response.EnsureSuccessStatusCode();
            // var content = await response.Content.ReadAsStringAsync();
            // int.TryParse(content, out var stock);
            // return stock;
        }
    }
}