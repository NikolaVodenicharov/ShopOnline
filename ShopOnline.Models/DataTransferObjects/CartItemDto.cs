namespace ShopOnline.Models.DataTransferObjects
{
    public class CartItemDto
    {
        public CartItemDto(int id, int cartId, int productId, string productName, string productDescription, string productImageUrl, decimal price, decimal totalPrice, int quantity)
        {
            Id = id;
            CartId = cartId;
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductImageUrl = productImageUrl;
            Price = price;
            TotalPrice = totalPrice;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
