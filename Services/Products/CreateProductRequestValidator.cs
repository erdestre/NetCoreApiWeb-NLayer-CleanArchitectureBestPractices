using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
	public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
	{
        public CreateProductRequestValidator()
        {
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Ürün ismi gereklidir.")
				.Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");

			RuleFor(x => x.Price)
				.GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

			//stock inclusiveBetween validation
			RuleFor(x => x.Stock)
				.InclusiveBetween(1, 100).WithMessage("Stok Adedi 1 ile 100 Arasında Olmalıdır.");
		}
    }
}
