using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Api.Repositories.ShoppingCarts
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext _dbContext;

        public ShoppingCartRepository(ShopOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto)
        {
            var cartItemExist = await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId);

            if (cartItemExist)
            {
                return null;
            }

            var productExist = await ProductExist(cartItemToAddDto.ProductId);

            if (!productExist)
            {
                return null;
            }

            var cartItem = new CartItem
            {
                CartId = cartItemToAddDto.CartId,
                ProductId = cartItemToAddDto.ProductId,
                Quantity = cartItemToAddDto.Quantity
            };

            await _dbContext.CartItems.AddAsync(cartItem);

            await _dbContext.SaveChangesAsync();

            var cartItemDto = await GetCartItemDto(cartItem);

            return cartItemDto;
        }

        public async Task<CartItem> GetItemAsync(int id)
        {
            var cartItemResult = await
                (from cart in _dbContext.Carts
                 join cartItem in _dbContext.CartItems
                 on cart.Id equals cartItem.Id
                 where cartItem.Id == id
                 select new CartItem
                 {
                     Id = cartItem.Id,
                     ProductId = cartItem.ProductId,
                     Quantity = cartItem.Quantity,
                     CartId = cartItem.CartId
                 })
                 .SingleOrDefaultAsync();

            return cartItemResult;
        }

        public async Task<IEnumerable<CartItemDto>> GetItemsAsync(int userId)
        {
            var cartItems = await
                (from cart in _dbContext.Carts
                 join cartItem in _dbContext.CartItems
                 on cart.Id equals cartItem.CartId
                 join product in _dbContext.Products
                 on cartItem.ProductId equals product.Id
                 where cart.UserId == userId
                 select new CartItemDto(
                     cartItem.Id,
                     cart.Id,
                     product.Id,
                     product.Name,
                     product.Description,
                     product.ImageUrl,
                     product.Price,
                     product.Price * cartItem.Quantity,
                     cartItem.Quantity)
                 )
                 .ToListAsync();

            return cartItems;
        }

        public async Task<CartItemDto> UpdateItemQuantityAsync(int id, CartItemQuantityUpdateDto cartItemQuantityUpdateDto)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return null;
            }

            cartItem.Quantity = cartItemQuantityUpdateDto.Quantity;

            await _dbContext.SaveChangesAsync();

            var cartItemDto = await GetCartItemDto(cartItem);

            return cartItemDto;
        }

        public async Task<CartItemDto> DeleteItemAsync(int id)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return null;
            }

            _dbContext.CartItems.Remove(cartItem);

            await _dbContext.SaveChangesAsync();

            var cartItemDto = await GetCartItemDto(cartItem);

            return cartItemDto;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            var exist = await _dbContext
                .CartItems
                .AnyAsync(ci =>
                    ci.CartId == cartId &&
                    ci.ProductId == productId);

            return exist;
        }

        private async Task<bool> ProductExist(int productId)
        {
            var exist = await _dbContext
                .Products
                .AnyAsync(p => p.Id == productId);

            return exist;
        }

        private async Task<CartItemDto> GetCartItemDto(CartItem cartItem)
        {
            var product = await _dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

            if (product == null)
            {
                return null;
            }

            var cartItemDto = new CartItemDto(
                cartItem.Id,
                cartItem.CartId,
                cartItem.ProductId,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Price,
                product.Price * cartItem.Quantity,
                cartItem.Quantity);

            return cartItemDto;
        }

        private async Task<CartItemDto> GetCartItemDto(int id)
        {
            var cartItemDto = await
                (from cart in _dbContext.Carts
                 join cartItem in _dbContext.CartItems
                 on cart.Id equals cartItem.CartId
                 join product in _dbContext.Products
                 on cartItem.ProductId equals product.Id
                 where cart.Id == id
                 select new CartItemDto(
                     cartItem.Id,
                     cart.Id,
                     product.Id,
                     product.Name,
                     product.Description,
                     product.ImageUrl,
                     product.Price,
                     product.Price * product.Quantity,
                     product.Quantity)
                 )
                 .FirstOrDefaultAsync();

            return cartItemDto;
        }
    }
}
