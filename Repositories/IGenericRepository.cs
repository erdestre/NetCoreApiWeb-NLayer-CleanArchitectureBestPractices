﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public interface IGenericRepository<T, TId> where T : class where TId : struct
	{
	    Task<bool> AnyAsync(TId id);
		IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T,bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id); 
        ValueTask AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
