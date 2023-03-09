namespace ShopOnline.Api.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string IconCss { get; set; } = string.Empty;

        public IEnumerable<Product> Products { get; set; }
    }
}
