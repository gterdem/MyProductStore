using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Xunit;

namespace MyProductStore.Products;

public class ProductAppServiceTests : MyProductStoreApplicationTestBase
{
    private readonly IProductAppService _productAppService;
    private readonly IDataFilter _dataFilter;

    public ProductAppServiceTests()
    {
        _dataFilter = GetRequiredService<IDataFilter>();
        _productAppService = GetService<IProductAppService>();
    }

    [Fact]
    public async Task Service_Should_Get_Available_And_Unavailable_Items()
    {
        // Unavailable item from ProductDataSeedContributor and available items from MyProductStoreTestDataSeedContributor
        var availableItems = await _productAppService.GetProductsByAvailabilityAsync(true);
        availableItems.Count.ShouldBe(3);

        var unAvailableItems = await _productAppService.GetProductsByAvailabilityAsync(false);
        unAvailableItems.Count.ShouldBe(0); // Unavailable items are filtered
    }

    [Fact]
    public async Task Service_Should_Use_SoftDelete_Data_Filter()
    {
        var existingProducts =
            await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

        var firstProduct = existingProducts.Items.First();
        firstProduct.ShouldNotBeNull();

        await _productAppService.DeleteAsync(firstProduct.Id);

        var remainingProducts =
            await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

        remainingProducts.Items.Count.ShouldBe(existingProducts.Items.Count - 1);

        using (_dataFilter.Disable<ISoftDelete>())
        {
            var allProducts =
                await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            allProducts.Items.Count.ShouldBeEquivalentTo(existingProducts.Items.Count);
        }
    }

    [Fact]
    public async Task Service_Should_Availability_Data_Filter()
    {
        var existingProducts =
            await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

        using (_dataFilter.Disable<IIsAvailable>())
        {
            var allProducts =
                await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            allProducts.Items.Count.ShouldBeEquivalentTo(existingProducts.Items.Count + 1);
        }
    }
}