using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyProductStore.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MyProductStore.Products;

public class EfCoreProductRepository : EfCoreRepository<MyProductStoreDbContext, Product, Guid>, IProductRepository
{
    public EfCoreProductRepository(IDbContextProvider<MyProductStoreDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<List<Product>> GetListByAvailability(bool isAvailable,
        CancellationToken cancellationToken = default)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .Where(q => q.IsAvailable == isAvailable)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}