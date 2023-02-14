namespace ShopOnline.Models.DataTransferObjects
{
    public record CartItemDto(
        int Id,
        int CartId,
        int ProductId,
        string ProductName,
        string ProductDescription,
        string ProductImageUrl,
        decimal Price,
        decimal TotalPrice,
        int Quantity);
}
