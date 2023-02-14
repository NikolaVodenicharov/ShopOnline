using ShopOnline.Api.Entities;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Repositories.ShoppingCarts
{
    public interface IShoppingCartRepository
    {
        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);

        Task<CartItem> UpdateItemAsync(int id, CartItemQuantityUpdateDto cartItemQuantityUpdateDto);

        Task<CartItem> GetItemAsync(int id);

        Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId);

        Task<CartItemDto> DeleteItemAsync(int id);
    }
}
