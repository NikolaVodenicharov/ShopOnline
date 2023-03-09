using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Repositories.Products;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var productDtos = await _productRepository.GetProductDtosAsync();

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct(int id)
        {
            var productDto = await _productRepository.GetProductDtoByIdAsync(id);

            return Ok(productDto);
        }

        [HttpGet(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            var productCategoriesDtos = await _productRepository.GetProductCategoriesDtosAsync();

            return Ok(productCategoriesDtos);
        }

        [HttpGet("ProductsByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> ProductsByCategory(int categoryId)
        {
            var productDtos = await _productRepository.GetProductsDtosByCategoryAsync(categoryId);

            return Ok(productDtos);
        }    
    }
}
