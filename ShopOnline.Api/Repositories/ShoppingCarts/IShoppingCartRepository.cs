using ShopOnline.Api.Entities;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Repositories.ShoppingCarts
{
    public interface IShoppingCartRepository
    {
        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);

        Task<CartItem> GetItemAsync(int id);

        Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId);

        Task<CartItemDto> UpdateItemQuantityAsync(int id, CartItemQuantityUpdateDto cartItemQuantityUpdateDto);

        Task<CartItemDto> DeleteItemAsync(int id);
    }
}
