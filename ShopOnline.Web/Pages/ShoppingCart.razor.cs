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

            CalculateTotalPrice();
        }

        private async Task UpdateCartItemQuantity(int id, int quantity)
        {
            if (quantity > 0)
            {
                var cartItemQuantityUpdateDto = new CartItemQuantityUpdateDto(id, quantity);

                var updatedCartItemDto = await ShoppingCartsService.UpdateQuantity(cartItemQuantityUpdateDto);

                UpdateItemTotalPrice(updatedCartItemDto);

                CalculateTotalPrice();
            }
        }

        private async Task DeleteCartItem(int id)
        {
            await ShoppingCartsService.DeleteItemAsync(id);

            var cartItemDto = CartItemDtos.Single(ci => ci.Id == id);

            CartItemDtos.Remove(cartItemDto);

            CalculateTotalPrice();
        }

        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = CartItemDtos.FirstOrDefault(ci => ci.Id == cartItemDto.Id);

            if (item == null)
            {
                return;
            }

            item.TotalPrice = cartItemDto.Price * cartItemDto.Quantity;
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = CartItemDtos.Sum(p => p.TotalPrice);
        }
    }
}
