using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ProductsServices;

namespace ShopOnline.Web.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        private IProductsService _productsService { get; set; } = default!;

        private const string LoadingMessage = "Loading product details...";

        [Parameter]
        public int Id { get; set; }

        public ProductDto Product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Product = await _productsService.GetProductByIdAsync(Id);
        }
    }
}
