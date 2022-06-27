using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MyProductStore.Products;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<List<Product>> GetListByAvailability(bool isAvailable, CancellationToken cancellationToken = default);
}