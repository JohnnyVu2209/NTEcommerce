using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Model.Identity;
using NTEcommerce.WebAPI.Repository.Interface;

namespace NTEcommerce.WebAPI.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EcommerceDbContext context) : base(context)
        {
        }
    }
}
