using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products
{
	public class ProductRepository(AppDbContext context) : GenericRepository<Product, int>(context), IProductRepository
	{
		public Task<List<Product>> GetTopSellingProductsAsync(int count) => Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
	}
}
