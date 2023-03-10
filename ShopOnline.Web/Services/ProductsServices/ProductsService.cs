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

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductDto>($"Products/{id}");

            return product;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("Products/GetProductCategories");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductCategoryDto>();
                }

                var productCategories = await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();

                return productCategories;
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtosByCategoryAsync(int categoryId)
        {
            var response = await _httpClient.GetAsync($"Products/ProductsByCategory/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductDto>();
                }

                var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();

                return products;
            }

            throw new Exception(response.StatusCode.ToString());
        }
    }
}
