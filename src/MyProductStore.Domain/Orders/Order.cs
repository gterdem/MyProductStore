using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyProductStore.Orders;

public class Order: FullAuditedAggregateRoot<Guid>
{
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
    public Guid CustomerId { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
}