using System.ComponentModel.DataAnnotations;

namespace MyProductStore.Products;

public class CreateProductDto
{
    [Required]
    [StringLength(ProductConstants.NameMaxLength)]
    public string Name { get; set; }

    [Required] public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
}