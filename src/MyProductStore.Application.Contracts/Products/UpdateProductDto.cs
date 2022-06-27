using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace MyProductStore.Products;

public class UpdateProductDto : EntityDto<Guid>
{
    [Required]
    [StringLength(ProductConstants.NameMaxLength)]
    public string Name { get; set; }

    [Required] public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}