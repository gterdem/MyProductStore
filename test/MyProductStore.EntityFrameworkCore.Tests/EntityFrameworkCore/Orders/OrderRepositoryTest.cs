using System.Linq;
using System.Threading.Tasks;
using MyProductStore.Orders;
using Shouldly;
using Xunit;

namespace MyProductStore.EntityFrameworkCore.Orders;

public class OrderRepositoryTest: MyProductStoreEntityFrameworkCoreTestBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly TestData _testData;

    public OrderRepositoryTest()
    {
        _orderRepository = GetRequiredService<IOrderRepository>();
        _testData = GetRequiredService<TestData>();
    }


    [Fact]
    public async Task Gets_Customer_Orders()
    {
        var customerOrders = await _orderRepository.GetOrdersByCustomerIdAsync(
            _testData.CustomerId, false
            );
        customerOrders.ShouldNotBeEmpty();
    }
    
    [Fact]
    public async Task Gets_Customer_Orders_With_OrderLines()
    {
        var customerOrders = await _orderRepository.GetOrdersByCustomerIdAsync(
            _testData.CustomerId
        );
        customerOrders.First().OrderLines.ShouldNotBeEmpty();
    }
}