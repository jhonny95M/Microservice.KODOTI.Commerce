using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System.Text;
using System.Text.Json;

namespace Order.Service.Proxies.Catalog
{
    
    public class CatalogHttpProxy: ICatalogProxy
    {
        private readonly ApiUrls apiUrls;
        private readonly HttpClient httpClient;

        public CatalogHttpProxy(IOptions<ApiUrls> options, HttpClient httpClient)
        {
            this.apiUrls = options.Value;
            this.httpClient = httpClient;
        }

        public async Task UpdateStockAsync(ProductInStockUpdateCommand command)
        {
            //ProductInStock
            var contend = new StringContent(
                JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
            var request = await httpClient.PutAsync(apiUrls.CatalogUrl + "api/productInStock", contend);
            request.EnsureSuccessStatusCode();
        }
    }
}
