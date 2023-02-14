using ShopOnline.Models.DataTransferObjects;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services.ShoppingCartsServices
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>("ShoppingCarts", cartItemToAddDto);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CartItemDto);
                }

                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            return await response.Content.ReadFromJsonAsync<CartItemDto>();
        }

        public async Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId)
        {
            var items = await _httpClient.GetFromJsonAsync<IEnumerable<CartItemDto>>($"ShoppingCarts/{userId}/GetItems");

            return items;
        }
    }
}
