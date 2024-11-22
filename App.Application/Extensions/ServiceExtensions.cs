﻿using System.Reflection;
using App.Application.Features.Categories;
using App.Application.Features.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			
            //TO DO: API tarafına taşınacak

            //services.AddScoped(typeof(NotFoundFilter<,>));
			//services.AddExceptionHandler<CriticalExceptionHandler>();
			//services.AddExceptionHandler<GlobalExceptionHandler>();

			services.AddFluentValidationAutoValidation();  // async yapacaksan otomatik eklenmiyor elle eklemen gerekiyor ve burayı implement etmemek gerekiyor.

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
		}
	}
}