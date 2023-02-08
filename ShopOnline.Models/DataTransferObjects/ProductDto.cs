using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Models.DataTransferObjects
{
    public record ProductDto(
        int Id, 
        string Name, 
        string Description, 
        string ImageUrl, 
        decimal Price, 
        int Quantity, 
        int CategoryId, 
        string CategoryName);
}
