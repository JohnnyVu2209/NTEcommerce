using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;

namespace NTEcommerce.WebAPI.Repository.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository

    {
        public ProductRepository(EcommerceDbContext context) : base(context)
        {
        }

        private IIncludableQueryable<Product, ICollection<ProductReview>?> GetAllInclude()
        {
            return context.Products.Include(p => p.Category).Include(p => p.Images).Include(p => p.Reviews);
        }

        
        public Task<IQueryable<Product>> FindByCategoryName(string name)
        {
            return Task.FromResult(context.Products.Include(p => p.Category).Include(x => x.Images).Where(x => x.Category != null && x.Category.Name.Contains(name)));
        }

        public Task<Product?> FindById(Guid id)
        {
            return GetAllInclude().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<IQueryable<Product>> FindAllWithInclude()
        {
            return (Task<IQueryable<Product>>)GetAllInclude().AsQueryable();
        }
    }
}
