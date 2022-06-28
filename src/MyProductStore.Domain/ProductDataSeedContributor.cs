using System;
using System.Threading.Tasks;
using MyProductStore.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace MyProductStore;

public class ProductDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Product, Guid> _productRepository;

    public ProductDataSeedContributor(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _productRepository.GetCountAsync() == 0)
        {
            await CreateTestProductAsync();
        }
    }

    private async Task CreateTestProductAsync()
    {
        var testProduct = new Product
        {
            Name = "Test Product",
            Price = new decimal(14.50),
            IsAvailable = false
        };

        await _productRepository.InsertAsync(testProduct);
    }
}