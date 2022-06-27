using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyProductStore.Orders;

public class OrderLine : CreationAuditedEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public override object[] GetKeys()
    {
        return new object[] {OrderId, ProductId};
    }
}