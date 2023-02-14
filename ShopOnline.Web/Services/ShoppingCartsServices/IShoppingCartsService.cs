using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Web.Services.ShoppingCartsServices
{
    public interface IShoppingCartsService
    {
        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);

        Task<ICollection<CartItemDto>> GetItemsAsync(int userId);

        Task<CartItemDto> UpdateQuantity(CartItemQuantityUpdateDto cartItemQuantityUpdateDto);

        Task<CartItemDto> DeleteItemAsync(int id);
    }
}
