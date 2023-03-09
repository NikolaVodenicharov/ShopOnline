using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Web.Services.ProductsServices
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesAsync();

        Task<IEnumerable<ProductDto>> GetProductsDtosByCategoryAsync(int categoryId);
    }
}
