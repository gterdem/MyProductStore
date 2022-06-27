using System;
using System.Threading.Tasks;
using MyProductStore.Orders;
using MyProductStore.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace MyProductStore;

public class MyProductStoreTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IdentityUserManager _identityUserManager;
    private readonly TestData _testData;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public MyProductStoreTestDataSeedContributor(
        IdentityUserManager identityUserManager,
        TestData testData,
        IProductRepository productRepository,
        IOrderRepository orderRepository)
    {
        _identityUserManager = identityUserManager;
        _testData = testData;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await CreateSampleUserAsync();
        await CreateSampleDataAsync();
    }

    private async Task CreateSampleUserAsync()
    {
        var sampleCustomer = await _identityUserManager.FindByEmailAsync(_testData.CustomerEmail);
        if (sampleCustomer == null)
        {
            await _identityUserManager.CreateAsync(
                new IdentityUser(_testData.CustomerId, _testData.CustomerEmail, _testData.CustomerEmail)
            );
        }
    }

    private async Task CreateSampleDataAsync()
    {
        var product1 = await _productRepository.InsertAsync(new Product
        {
            Name = "Sample Product - 1",
            Price = new decimal(159),
            IsAvailable = true
        });
        var product2 = await _productRepository.InsertAsync(new Product
        {
            Name = "Sample Product - 2",
            Price = new decimal(24),
            IsAvailable = true
        });
        var product3 = await _productRepository.InsertAsync(new Product
        {
            Name = "Sample Product - 3",
            Price = new decimal(2),
            IsAvailable = true
        });

        var newOrder = new Order
        {
            Date = DateTime.Now,
            Status = OrderStatus.Accepted,
            CustomerId = _testData.CustomerId
        };

        newOrder.OrderLines.Add(new OrderLine()
        {
            OrderId = newOrder.Id,
            ProductId = product1.Id,
            Quantity = 12
        });
        newOrder.OrderLines.Add(new OrderLine()
        {
            OrderId = newOrder.Id,
            ProductId = product2.Id,
            Quantity = 4
        });
        newOrder.OrderLines.Add(new OrderLine()
        {
            OrderId = newOrder.Id,
            ProductId = product3.Id,
            Quantity = 1
        });

        await _orderRepository.InsertAsync(newOrder);
    }
}