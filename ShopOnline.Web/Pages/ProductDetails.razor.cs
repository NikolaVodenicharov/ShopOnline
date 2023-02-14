using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ProductsServices;
using ShopOnline.Web.Services.ShoppingCartsServices;
using System.Diagnostics;

namespace ShopOnline.Web.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        private IProductsService ProductsService { get; set; } = default!;

        [Inject]
        private IShoppingCartsService ShoppingCartsService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private const string LoadingMessage = "Loading product details...";

        [Parameter]
        public int Id { get; set; }

        public ProductDto Product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Product = await ProductsService.GetProductByIdAsync(Id);
        }

        private async Task AddToCartClick(CartItemToAddDto cartItemToAddDto)
        {
            var cartItemDto = await ShoppingCartsService.AddItemAsync(cartItemToAddDto);

            NavigationManager.NavigateTo("/ShoppingCart");
        }
    }
}
