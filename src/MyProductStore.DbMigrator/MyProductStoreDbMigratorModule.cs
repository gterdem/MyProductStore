using MyProductStore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MyProductStore.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MyProductStoreEntityFrameworkCoreModule),
    typeof(MyProductStoreApplicationContractsModule)
    )]
public class MyProductStoreDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
