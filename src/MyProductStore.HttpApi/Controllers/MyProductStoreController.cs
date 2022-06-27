using MyProductStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyProductStore.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MyProductStoreController : AbpControllerBase
{
    protected MyProductStoreController()
    {
        LocalizationResource = typeof(MyProductStoreResource);
    }
}
