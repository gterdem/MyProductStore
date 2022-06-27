using System;
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
    public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
    {
        GetListPolicyName = MyProductStorePermissions.Products.Default;
        GetPolicyName = MyProductStorePermissions.Products.Default;
        CreatePolicyName = MyProductStorePermissions.Products.Create;
        UpdatePolicyName = MyProductStorePermissions.Products.Edit;
        DeletePolicyName = MyProductStorePermissions.Products.Delete;
    }
}