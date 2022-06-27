using System.Threading.Tasks;

namespace MyProductStore.Data;

public interface IMyProductStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
