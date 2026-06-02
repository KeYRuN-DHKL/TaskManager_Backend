using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => 
            await _dbset.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => 
            await _dbset.ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> predicate) => 
            await _dbset.Where(predicate).ToListAsync();

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
            await _dbset.FirstOrDefaultAsync(predicate);

        public async Task AddAsync(T entity) =>
            await _dbset.AddAsync(entity);

        public void Update(T entity) =>
             _dbset.Update(entity);

        public void Remove(T entity) =>
            _dbset.Remove(entity);

        public IQueryable<T> Query() =>
            _dbset.AsQueryable();
    }
}
