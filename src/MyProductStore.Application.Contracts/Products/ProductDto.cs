using System;
using Volo.Abp.Application.Dtos;

namespace MyProductStore.Products;

public class ProductDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}