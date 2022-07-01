using NTEcommerce.WebAPI.Model;

namespace NTEcommerce.WebAPI.Repository.Interface
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IQueryable<Product>> FindByCategoryName(string name);
        Task<Product> FindById(Guid id);
        Task<IQueryable<Product>> FindAllWithInclude();
    }
}
