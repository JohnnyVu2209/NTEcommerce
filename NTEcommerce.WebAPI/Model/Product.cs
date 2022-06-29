namespace NTEcommerce.WebAPI.Model
{
    public class Product: BaseEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductImage>? Images { get; set; }
        public virtual ICollection<ProductReview>? Reviews { get; set; }
    }
}
