using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DataTransferObjects;

namespace ShopOnline.Web.Pages
{
    public partial class DisplayProducts
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
    }
}
