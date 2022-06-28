using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyProductStore.Products;

public class Product : FullAuditedEntity<Guid>, IIsAvailable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}