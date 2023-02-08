using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Repositories.Interfaces;
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
            var products = await _productRepository.GetProductDtosAsync();

            return Ok(products);
        }
    }
}
