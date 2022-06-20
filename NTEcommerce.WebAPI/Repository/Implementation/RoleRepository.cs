using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Model.Identity;
using NTEcommerce.WebAPI.Repository.Interface;

namespace NTEcommerce.WebAPI.Repository.Implementation
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(EcommerceDbContext context) : base(context)
        {
        }
    }
}
