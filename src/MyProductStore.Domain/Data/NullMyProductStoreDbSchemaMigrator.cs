using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MyProductStore.Data;

/* This is used if database provider does't define
 * IMyProductStoreDbSchemaMigrator implementation.
 */
public class NullMyProductStoreDbSchemaMigrator : IMyProductStoreDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
