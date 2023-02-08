using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Web.Pages
{
    public partial class DisplayProductsRow
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
    }
}
