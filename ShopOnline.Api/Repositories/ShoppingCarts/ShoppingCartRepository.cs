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

            var result = await _dbContext.CartItems.AddAsync(cartItem);

            await _dbContext.SaveChangesAsync();

            var product = await _dbContext
                .Products
                .FirstOrDefaultAsync(p => p.Id == cartItemToAddDto.ProductId);

            if (product == null)
            {
                return null;
            }

            var cartItemDto = new CartItemDto(
                result.Entity.Id,
                result.Entity.CartId,
                result.Entity.ProductId,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Price,
                product.Price * result.Entity.Quantity,
                result.Entity.Quantity);

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

        public Task DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
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

        //public async Task<IEnumerable<CartItem>> GetItemsAsync(int userId)
        //{
        //    var cartItems = await
        //        (from cart in _dbContext.Carts
        //         join cartItem in _dbContext.CartItems
        //         on cart.Id equals cartItem.CartId
        //         where cart.UserId == userId
        //         select new CartItem
        //         {
        //             Id = cartItem.Id,
        //             ProductId= cartItem.ProductId,
        //             Quantity= cartItem.Quantity,
        //             CartId= cartItem.CartId,
        //         })
        //         .ToListAsync();

        //    return cartItems;
        //}

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
                     product.Price * product.Quantity,
                     product.Quantity)
                 )
                 .ToListAsync();

            return cartItems;
        }

        public Task<CartItem> UpdateItemAsync(int id, CartItemQuantityUpdateDto cartItemQuantityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
