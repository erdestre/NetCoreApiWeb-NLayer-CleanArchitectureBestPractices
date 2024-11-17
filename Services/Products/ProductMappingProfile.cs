using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;

namespace App.Services.Products
{
    internal class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dect => dect.Name,
                    opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(dect => dect.Name,
                    opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
