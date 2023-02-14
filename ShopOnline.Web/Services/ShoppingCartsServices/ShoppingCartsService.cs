using Newtonsoft.Json;
using ShopOnline.Models.DataTransferObjects;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

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

        public async Task<ICollection<CartItemDto>> GetItemsAsync(int userId)
        {
            var items = await _httpClient.GetFromJsonAsync<ICollection<CartItemDto>>($"ShoppingCarts/{userId}/GetItems");

            return items;
        }

        public async Task<CartItemDto> UpdateQuantity(CartItemQuantityUpdateDto cartItemQuantityUpdateDto)
        {
            var jsonRequest = JsonConvert.SerializeObject(cartItemQuantityUpdateDto);
            
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync(
                $"ShoppingCarts/{cartItemQuantityUpdateDto.CartItemId}",
                content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var cartItemDto = await response.Content.ReadFromJsonAsync<CartItemDto>();

            return cartItemDto;
        }

        public async Task<CartItemDto> DeleteItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"ShoppingCarts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return default(CartItemDto);
            }

            var cartItemDto = await response.Content.ReadFromJsonAsync<CartItemDto>();

            return cartItemDto;

            //var cartItemDto = await _httpClient.DeleteFromJsonAsync<CartItemDto>($"ShoppingCarts/{id}");

            //return cartItemDto;
        }
    }
}
