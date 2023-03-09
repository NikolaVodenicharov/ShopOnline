using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;
using ShopOnline.Web.Services.ProductsServices;

namespace ShopOnline.Web.Pages
{
    public partial class ProductsByCategory
    {
        [Inject]
        private IProductsService ProductsService { get; set; } = default!;

        [Parameter]
        public int CategoryId { get; set; }

        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();

        public string CategoryName { get; set; } = string.Empty;

        protected override async Task OnParametersSetAsync()
        {
            Products = await ProductsService.GetProductsDtosByCategoryAsync(CategoryId);

            var productsExist = Products != null && Products.Any();

            if (productsExist)
            {
                var product = Products.FirstOrDefault(p => p.CategoryId == CategoryId);

                if (product != null)
                {
                    CategoryName = product.CategoryName;
                }
            }
        }
    }
}
