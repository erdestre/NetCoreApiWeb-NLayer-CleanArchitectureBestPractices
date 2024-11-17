﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;

namespace App.Services.Categories
{
    public class CategoryMappingProfile : Profile
	{
		public CategoryMappingProfile()
		{
			CreateMap<CategoryDto, Category>().ReverseMap();
			CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
			CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src=> src.Name.ToLowerInvariant()));
			CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
		}
	}
}