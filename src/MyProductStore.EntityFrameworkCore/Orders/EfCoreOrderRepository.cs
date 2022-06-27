using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyProductStore.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MyProductStore.Orders;

public class EfCoreOrderRepository: EfCoreRepository<MyProductStoreDbContext, Order, Guid>, IOrderRepository
{
    public EfCoreOrderRepository(IDbContextProvider<MyProductStoreDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<Order>> GetOrdersByCustomerIdAsync(Guid customerId, bool include = true, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .IncludeDetails(include)
            .Where(q => q.CustomerId == customerId)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
    
    public async override Task<IQueryable<Order>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}