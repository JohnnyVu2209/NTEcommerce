namespace NTEcommerce.WebAPI.Repository.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        int Save();
        Task SaveAsync();
    }
}
