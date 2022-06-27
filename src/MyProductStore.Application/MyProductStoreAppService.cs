using System;
using System.Collections.Generic;
using System.Text;
using MyProductStore.Localization;
using Volo.Abp.Application.Services;

namespace MyProductStore;

/* Inherit your application services from this class.
 */
public abstract class MyProductStoreAppService : ApplicationService
{
    protected MyProductStoreAppService()
    {
        LocalizationResource = typeof(MyProductStoreResource);
    }
}
