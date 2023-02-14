using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Repositories.Products;
using ShopOnline.Api.Repositories.ShoppingCarts;
using ShopOnline.Models.DataTransferObjects;
using System.Reflection.Metadata.Ecma335;

namespace ShopOnline.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartsController(
            IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            var cartItemDtos = await _shoppingCartRepository.GetItemsAsync(userId);

            return Ok(cartItemDtos);
        }

        //[HttpGet("{id}:int")]
        //public async Task<ActionResult<CartItemDto>> GetItem(int id)
        //{

        //}

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            var cartItemDto = await _shoppingCartRepository.AddItemAsync(cartItemToAddDto);

            if(cartItemDto == null)
            {
                return NoContent();
            }

            return Ok(cartItemDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            var cartItemDto = await _shoppingCartRepository.DeleteItemAsync(id);

            return Ok(cartItemDto);
        }
    }
}
