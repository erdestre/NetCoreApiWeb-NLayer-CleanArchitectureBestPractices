﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi gereklidir.")
                .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
            //.Must(isUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
            //.MustAsync(isUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
	            .GreaterThan(0).WithMessage("Ürün kategori değeri 0'dan büyük olmalıdır.");

			//stock inclusiveBetween validation
			RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok Adedi 1 ile 100 Arasında Olmalıdır.");
        }

        //private async Task<bool> isUniqueProductName(string Name, CancellationToken cancellationToken)
        //{
        //    return !await _productRepository.Where(x => x.Name == Name).AnyAsync(cancellationToken);
        //}
        //private bool isUniqueProductName(string name)
        //      {
        //          return !_productRepository.Where(x => x.Name == name).Any();
        //      }


    }
}