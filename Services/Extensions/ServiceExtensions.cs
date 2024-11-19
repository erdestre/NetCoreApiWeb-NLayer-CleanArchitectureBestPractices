using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using App.Services.Categories;
using App.Services.ExceptionHandlers;
using App.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped(typeof(NotFoundFilter<,>));

			services.AddFluentValidationAutoValidation();  // async yapacaksan otomatik eklenmiyor elle eklemen gerekiyor ve burayı implement etmemek gerekiyor.

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddExceptionHandler<CriticalExceptionHandler>();
			services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
		}
	}
}
