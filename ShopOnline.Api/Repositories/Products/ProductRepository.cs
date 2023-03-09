using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _dbContext;

        public ProductRepository(ShopOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDto> GetProductDtoByIdAsync(int id)
        {
            var productDto = await _dbContext
                .Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.Price,
                    p.Quantity,
                    p.CategoryId,
                    p.Category.Name))
                .FirstOrDefaultAsync();

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductDtosAsync()
        {
            var productDtos = await _dbContext
                .Products
                .Include(p => p.Category)
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.Price,
                    p.Quantity,
                    p.CategoryId,
                    p.Category.Name))
                .ToListAsync();

            return productDtos;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategoriesDtosAsync()
        {
            var productCategoriesDtos = await
                (from ProductCategory in _dbContext.ProductCategories
                 select new ProductCategoryDto(
                    ProductCategory.Id,
                    ProductCategory.Name,
                    ProductCategory.IconCss)
                 )
                 .ToListAsync();

            return productCategoriesDtos;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtosByCategoryAsync(int categoryId)
        {
            var productDtos = await _dbContext
                .Products
                .Where(p => p.CategoryId== categoryId)
                .Include(p => p.Category)
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.Price,
                    p.Quantity,
                    p.CategoryId,
                    p.Category.Name))
                .ToListAsync();

            return productDtos;
        }
    }
}
