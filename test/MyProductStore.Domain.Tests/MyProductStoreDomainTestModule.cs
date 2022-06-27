using MyProductStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MyProductStore;

[DependsOn(
    typeof(MyProductStoreEntityFrameworkCoreTestModule)
    )]
public class MyProductStoreDomainTestModule : AbpModule
{

}
