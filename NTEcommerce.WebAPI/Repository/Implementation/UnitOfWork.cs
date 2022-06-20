using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Repository.Interface;

namespace NTEcommerce.WebAPI.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private EcommerceDbContext context;
        public UnitOfWork(EcommerceDbContext context)
        {
            this.context = context;
            User = new UserRepository(context);
            Role = new RoleRepository(context);
            Product = new ProductRepository(context);
            Category = new CategoryRepository(context);
        }

        public IUserRepository User { get; private set; }

        public IRoleRepository Role { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
