using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
	internal class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
	{
		public Task<List<Product>> GetTopSellingProductsAsync(int count) => Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
	}
}
