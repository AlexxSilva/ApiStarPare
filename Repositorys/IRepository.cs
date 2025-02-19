using System.Linq.Expressions;

namespace ApiStarPare.Repositorys
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
