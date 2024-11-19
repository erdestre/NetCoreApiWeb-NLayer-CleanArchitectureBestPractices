using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using App.Repositories.Products;
using App.Services.Filters;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductsController(IProductService productService) : CustomBaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllListAsync());
		[HttpGet("{pageNumber:int}/{pageSize:int}")]
		public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));
		// http://localhost:5000/api/products?id=2
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));
		[HttpPost]
		public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));
		[ServiceFilter(typeof(NotFoundFilter<Product, int>))]
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));
		[HttpPatch("Stock")]
		public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));
		//[HttpPut("UpdateStock")]
		//public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));
		[ServiceFilter(typeof(NotFoundFilter<Product, int>))]
		[HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
    }
}
