namespace ShopOnline.Models.DataTransferObjects
{
    public record CartItemQuantityUpdateDto(
        int CartItemId,
        int Quantity);
}
