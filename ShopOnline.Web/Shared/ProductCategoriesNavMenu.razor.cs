using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ProductsServices;

namespace ShopOnline.Web.Shared
{
    public partial class ProductCategoriesNavMenu
    {
        [Inject]
        public IProductsService ProductsService { get; set; }

        public IEnumerable<ProductCategoryDto> ProductCategories { get; set; } = new List<ProductCategoryDto>();

        protected override async Task OnInitializedAsync()
        {
            ProductCategories = await ProductsService.GetProductCategoriesAsync();
        }

    }
}
