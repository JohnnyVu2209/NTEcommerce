using NTEcommerce.WebAPI.Model;

namespace NTEcommerce.WebAPI.Repository.Interface
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<Category> FindByNameAsync(string name);
        Task<bool> CheckExistName(string name);
    }
}
