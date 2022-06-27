using NTEcommerce.WebAPI.Model.Identity;

namespace NTEcommerce.WebAPI.Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUser(string username, string password);
    }
}
