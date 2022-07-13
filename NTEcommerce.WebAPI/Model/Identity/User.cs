using Microsoft.AspNetCore.Identity;

namespace NTEcommerce.WebAPI.Model.Identity
{
    public class User: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Guid CartId { get; set; }
        public virtual ShoppingCart Cart { get; set; }
    }
}
