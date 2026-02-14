using Ambulance.Api.Infrastructures;
using System.Linq.Expressions;

namespace Ambulance.Api.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        Task<PagedResponse<T>> GetPagedAsync(Expression<Func<T, bool>>? predicate, int pageNumber, int pageSize);
    }
}
