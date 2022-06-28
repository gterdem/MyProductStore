using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProductStore.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MyProductStore.Products;

public class ProductAppService : CrudAppService<
    Product,
    ProductDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateProductDto,
    UpdateProductDto>, IProductAppService
{
    private readonly IProductRepository _productRepository;

    public ProductAppService(IRepository<Product, Guid> repository, IProductRepository productRepository) :
        base(repository)
    {
        _productRepository = productRepository;
        GetListPolicyName = MyProductStorePermissions.Products.Default;
        GetPolicyName = MyProductStorePermissions.Products.Default;
        CreatePolicyName = MyProductStorePermissions.Products.Create;
        UpdatePolicyName = MyProductStorePermissions.Products.Edit;
        DeletePolicyName = MyProductStorePermissions.Products.Delete;
    }

    public async Task<List<ProductDto>> GetProductsByAvailabilityAsync(bool isAvailable)
    {
        var products = await _productRepository.GetListByAvailability(isAvailable);

        return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
    }
}