namespace ShopOnline.Models.DataTransferObjects
{
    public record CartItemToAddDto(
        int CartId,
        int ProductId,
        int Quantity);
}
