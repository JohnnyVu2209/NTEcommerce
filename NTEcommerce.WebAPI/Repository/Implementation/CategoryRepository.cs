using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;

namespace NTEcommerce.WebAPI.Repository.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EcommerceDbContext context) : base(context)
        {
        }
    }
}
