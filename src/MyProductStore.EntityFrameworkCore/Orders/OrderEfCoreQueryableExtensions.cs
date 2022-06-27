using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyProductStore.Orders;

public static class OrderEfCoreQueryableExtensions
{
    public static IQueryable<Order> IncludeDetails(this IQueryable<Order> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(q => q.OrderLines);
    }
}