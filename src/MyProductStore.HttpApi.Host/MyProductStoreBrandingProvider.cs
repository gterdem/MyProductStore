using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MyProductStore;

[Dependency(ReplaceServices = true)]
public class MyProductStoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MyProductStore";
}
