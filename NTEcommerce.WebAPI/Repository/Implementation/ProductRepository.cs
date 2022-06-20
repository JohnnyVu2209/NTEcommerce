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
    }
}
