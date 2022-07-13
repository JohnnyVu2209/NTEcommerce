using NTEcommerce.WebAPI.Model.Identity;

namespace NTEcommerce.WebAPI.Model
{
    public class ShoppingCart:BaseEntity
    {
        public Guid Id { get; set; }
        public int Total { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
