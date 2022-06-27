using System;
using Volo.Abp.DependencyInjection;

namespace MyProductStore;

public class TestData: ISingletonDependency
{
    public Guid CustomerId { get; } = Guid.NewGuid();
    public string CustomerEmail { get; } = "customer@test.com";
}