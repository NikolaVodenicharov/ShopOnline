using ShopOnline.Models.DataTransferObjects;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services.ProductsServices
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;

        public ProductsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("Products");

            return products;
        }
    }
}
