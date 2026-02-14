using Ambulance.Api.Infrastructures;
using Ambulance.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ambulance.Api.Repositories
{
    public class Repository<T, TContext> : IRepository<T> where T : class where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
        public void Update(T entity) => _dbSet.Update(entity);

        public async Task<PagedResponse<T>> GetPagedAsync(Expression<Func<T, bool>>? predicate, int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            var total = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<T>(data, total, pageNumber, pageSize);
        }
    }
}
