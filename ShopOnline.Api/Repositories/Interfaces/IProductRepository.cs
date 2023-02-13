using ShopOnline.Api.Entities;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductDtosAsync();

        Task<ProductDto> GetProductDtoByIdAsync(int id);
    }
}
