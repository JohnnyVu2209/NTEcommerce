using NTEcommerce.WebAPI.Model.Identity;

namespace NTEcommerce.WebAPI.Model
{
    public class ProductReview
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public Product? Product { get; set; }
        public User? User { get; set; }
    }
}
