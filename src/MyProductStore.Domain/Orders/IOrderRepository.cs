using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MyProductStore.Orders;

public interface IOrderRepository : IRepository<Order, Guid>
{
    Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId, bool include = true, CancellationToken cancellationToken = default);
}