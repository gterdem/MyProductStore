using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MyProductStore.Products;

public class ProductAppServiceTests:MyProductStoreApplicationTestBase
{
    private readonly IProductAppService _productAppService;

    public ProductAppServiceTests()
    {
        _productAppService = GetRequiredService<IProductAppService>();
    }

    [Fact]
    public async Task Service_Should_Get_Unavailable_Items()
    {
        var result = await _productAppService.GetProductsByAvailabilityAsync(false);
        
        // Seeded test product data is available
        result.ShouldBeEmpty();
    }
}