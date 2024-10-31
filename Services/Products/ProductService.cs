using App.Repositories;
using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
		public Task<List<Product>> GetTopPriceProducts(int count) => productRepository.GetTopSellingProductsAsync(count);
    }
}
