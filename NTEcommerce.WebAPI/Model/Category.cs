namespace NTEcommerce.WebAPI.Model
{
    public class Category: BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? Name { get; set; }
        public int TotalProducts { get; set; }
        public string? Description { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}
