namespace NTEcommerce.WebAPI.Model
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public Product? Product { get; set; }
    }
}
