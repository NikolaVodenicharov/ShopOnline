using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ShoppingCartsServices;

namespace ShopOnline.Web.Pages
{
    public partial class ShoppingCart
    {
        [Inject]
        private IShoppingCartsService ShoppingCartsService { get; set; } = default!;

        public ICollection<CartItemDto> CartItemDtos { get; set; } = new List<CartItemDto>();

        public decimal TotalPrice { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CartItemDtos = await ShoppingCartsService.GetItemsAsync(HardCoded.UserId);

            TotalPrice = CalculateTotalPrice(CartItemDtos);
        }

        private async Task DeleteCartItem(int id)
        {
            await ShoppingCartsService.DeleteItemAsync(id);

            var cartItemDto = CartItemDtos.Single(ci => ci.Id == id);

            CartItemDtos.Remove(cartItemDto);
        }

        private decimal CalculateTotalPrice(IEnumerable<CartItemDto> CartItemDtos)
        {
            decimal totalPrice = 0;

            foreach (var cartItemDto in CartItemDtos)
            {
                totalPrice += cartItemDto.Price;
            }
            return totalPrice;
        }
    }
}
