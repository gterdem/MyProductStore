using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MyProductStore.Blazor;

[Dependency(ReplaceServices = true)]
public class MyProductStoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MyProductStore";
}
