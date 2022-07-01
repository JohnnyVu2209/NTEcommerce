using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CheckExistName(string name)
        {
            var category = await context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();

            if (category == null)
                return false;
            return true;
        }

        public async Task<Category> FindByIdAsync(Guid id)
        {
            return await context.Categories.Include(x => x.ParentCategory).Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
