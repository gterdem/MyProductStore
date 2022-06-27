using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MyProductStore.Products;

public interface IProductAppService : ICrudAppService<
    ProductDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateProductDto,
    UpdateProductDto
>
{
}