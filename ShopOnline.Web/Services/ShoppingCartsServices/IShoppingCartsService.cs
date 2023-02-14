using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Web.Services.ShoppingCartsServices
{
    public interface IShoppingCartsService
    {
        Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId);

        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);
    }
}
