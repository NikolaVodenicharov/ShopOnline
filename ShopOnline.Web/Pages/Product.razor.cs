using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ProductsServices;

namespace ShopOnline.Web.Pages
{
    public partial class Product
    {
        private const string loadingMessage = "Loading products...";
        private const int itemsPerRow = 4;

        [Inject]
        private IProductsService ProductsService { get; set; } = default!;

        private IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductsService.GetProductsAsync();
        }
    }
}
