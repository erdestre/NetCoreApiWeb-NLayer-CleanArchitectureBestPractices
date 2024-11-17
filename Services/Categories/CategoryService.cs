﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
		public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
		{
			var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId); 

			if (category is null)
			{
				return ServiceResult<CategoryWithProductsDto>.Fail("Category not found", HttpStatusCode.NotFound);
			}
			var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

			return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);	
		}
		public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
		{
			var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();

			var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

			return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
		}

		public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
		{
			var categories = await categoryRepository.GetAll().ToListAsync();

			var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);

			return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
		}
		public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
	    {
			var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
			if(anyCategory)
			{
				return ServiceResult<int>.Fail("Category already exists",HttpStatusCode.NotFound);
			};

			var newCategory = mapper.Map<Category>(request);

			await categoryRepository.AddAsync(newCategory);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult<int>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
		}
		public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
		{
			var category = await categoryRepository.GetByIdAsync(id);
			if (category == null)
			{
				return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
			}

			var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();

			if(isCategoryNameExist)
			{
				return ServiceResult.Fail("Category already exists", HttpStatusCode.NotFound);
			}

			category = mapper.Map(request, category);

			categoryRepository.Update(category);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult.Success(HttpStatusCode.NoContent);
		}
		public async Task<ServiceResult> DeleteAsync(int id)
		{
			var category = await categoryRepository.GetByIdAsync(id);

			if(category == null)
			{
				return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
			}

			categoryRepository.Delete(category);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult.Success(HttpStatusCode.NoContent);
		} 
		public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
		{
			var category = await categoryRepository.GetByIdAsync(id);

			if (category == null)
			{
				return ServiceResult<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);
			}

			var categoryAsDto = mapper.Map<CategoryDto>(category);

			return ServiceResult<CategoryDto>.Success(categoryAsDto);
		}


	}
}
