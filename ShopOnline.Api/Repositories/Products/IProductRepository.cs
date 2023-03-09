using ShopOnline.Api.Entities;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Repositories.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductDtosAsync();

        Task<ProductDto> GetProductDtoByIdAsync(int id);

        Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesDtosAsync();

        Task<IEnumerable<ProductDto>> GetProductsDtosByCategoryAsync(int categoryId);
    }
}
