﻿using App.Repositories.Categories;
using App.Repositories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Services.Categories.Dto;

namespace App.Services.Categories
{
    public interface ICategoryService
	{
		Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
		Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
		Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
		Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
		Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
		Task<ServiceResult> DeleteAsync(int id); 
		Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
	}
}
