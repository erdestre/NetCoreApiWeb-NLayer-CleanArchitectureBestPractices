using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
		protected AppDbContext Context = context; // bu kısmı normal yap bence
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async ValueTask AddAsync(T Entity) => await _dbSet.AddAsync(Entity);
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

        public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

        public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

        public void Update(T Entity) => _dbSet.Update(Entity);

        public void Delete(T Entity) => _dbSet.Remove(Entity);

    }
}



