using Volo.Abp.Modularity;

namespace MyProductStore;

[DependsOn(
    typeof(MyProductStoreApplicationModule),
    typeof(MyProductStoreDomainTestModule)
    )]
public class MyProductStoreApplicationTestModule : AbpModule
{

}
