using System.Threading.Tasks;
using MyProductStore.Products;
using Shouldly;
using Xunit;

namespace MyProductStore.EntityFrameworkCore.Products;

public class ProductEntityFrameworkCoreTests : MyProductStoreEntityFrameworkCoreTestBase
{
    private readonly IProductRepository _productRepository;

    public ProductEntityFrameworkCoreTests()
    {
        _productRepository = GetRequiredService<IProductRepository>();
    }

    [Fact]
    public async Task ProductRepository_Should_Return_Available_Items()
    {
        var availableItems = await _productRepository.GetListByAvailability(true);
        
        // Because test data is already seeded from the ProductDataSeedContributor
        availableItems.ShouldNotBeEmpty();
    }
}