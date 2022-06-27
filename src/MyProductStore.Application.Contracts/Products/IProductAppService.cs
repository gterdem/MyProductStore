using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    Task<List<ProductDto>> GetProductsByAvailabilityAsync(bool isAvailable);
}