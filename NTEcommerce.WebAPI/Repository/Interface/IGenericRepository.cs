using System.Linq.Expressions;

namespace NTEcommerce.WebAPI.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> FindAll();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        Task AddArrageAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteArrage(IEnumerable<T> entities);
    }
}
