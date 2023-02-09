using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ProductsServices;

namespace ShopOnline.Web.Pages
{
    public partial class Product
    {
        private const string loadingMessage = "Loading products...";

        [Inject]
        private IProductsService ProductsService { get; set; } = default!;

        private IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductsService.GetProductsAsync();
        }

        private IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            var result =
                from product in Products
                group product by product.CategoryId into productByCategoryGroup
                orderby productByCategoryGroup.Key
                select productByCategoryGroup;

            return result;
        }

        private string GetCategoryName(IGrouping<int, ProductDto> productGroup)
        {
            var result = productGroup
                .FirstOrDefault(pg => pg.CategoryId == productGroup.Key)
                .Name;

            return result;
        }
    }
}
