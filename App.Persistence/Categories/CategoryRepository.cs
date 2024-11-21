﻿using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Category>> GetCategoryWithProductsAsync()
        {
            return context.Categories.Include(c => c.Products).ToListAsync(); 
        }

        public IQueryable<Category> GetCategoryWithProducts()
        {
            return context.Categories.Include(c => c.Products).AsQueryable();
        }
    }
}
