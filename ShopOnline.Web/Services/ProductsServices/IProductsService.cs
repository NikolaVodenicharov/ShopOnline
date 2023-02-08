using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Web.Services.ProductsServices
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
