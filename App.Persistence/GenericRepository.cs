using System.Linq.Expressions;
using App.Application.Contracts.Persistence;
using App.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence
{
    public class GenericRepository<T, TId>(AppDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
	{
		protected AppDbContext Context = context; // bu kısmı normal yap bence
        private readonly DbSet<T> _dbSet = context.Set<T>();

		public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id!.Equals(id));
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async ValueTask AddAsync(T Entity) => await _dbSet.AddAsync(Entity);
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

        public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

        public void Update(T Entity) => _dbSet.Update(Entity);

        public void Delete(T Entity) => _dbSet.Remove(Entity);

    }
}



