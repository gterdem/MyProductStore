using MyProductStore.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MyProductStore.Blazor;

public abstract class MyProductStoreComponentBase : AbpComponentBase
{
    protected MyProductStoreComponentBase()
    {
        LocalizationResource = typeof(MyProductStoreResource);
    }
}
