using System.Net;
using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Contracts.ServiceBus;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using App.Domain.Events;
using AutoMapper;
using FluentValidation;

namespace App.Application.Features.Products;

public class  ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IValidator<CreateProductRequest> createProductRequestValidator, IMapper mapper, ICacheService cacheService, IServiceBus busService) : IProductService
{
    private const string ProductListCacheKey = "ProductListCacheKey";
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopSellingProductsAsync(count);

        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }
    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        // cache aside design pattern
        // 1. cache
        // 2. fromdb
        // 3. caching data

        //decorator design pattern / proxy design pattern

        var productListAsCached = await cacheService.GetAsync< List<ProductDto>> (ProductListCacheKey);
        if (productListAsCached is not null) return ServiceResult<List<ProductDto>>.Success(productListAsCached);
        
        var products = await productRepository.GetAllAsync();
        var productAsDto = mapper.Map<List<ProductDto>>(products);
        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        await cacheService.AddAsync(ProductListCacheKey, productAsDto, TimeSpan.FromMinutes(1));
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }
    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
        }
        //var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
        var productAsDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto?>.Success(productAsDto!);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);

        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }
    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        //2. way async manuel service business check 
        var anyProduct = await productRepository.AnyAsync(x => x.Name == request.Name);
        if (anyProduct) return ServiceResult<CreateProductResponse>.Fail("Product already exist", HttpStatusCode.BadRequest);

        //3.way async manuel validation business check
        //var validationResultAsync= await createProductRequestValidator.ValidateAsync(request); // bunu kullanıyosa

        //if (!validationResultAsync.IsValid)
        //{
        //    return ServiceResult<CreateProductResponse>.Fail(validationResultAsync.Errors.Select(x => x.ErrorMessage).ToList());
        //}

        var product = mapper.Map<Product>(request);

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        await busService.PublishAsync(new ProductAddedEvent(product.Id, product.Name, product.Price));

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }
    //fast fail - önce olumsuz durumları dönmek
    //Guard Clauses - Tüm olumsuz durumları if ile yaz cyclomatic complexity (code metric result)
    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {

        var isProductNameExist = await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);
        if (isProductNameExist) return ServiceResult.Fail("Product already exist", HttpStatusCode.BadRequest);

            
        var product = mapper.Map<Product>(request);
        product.Id = id;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product.Stock = request.Quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}