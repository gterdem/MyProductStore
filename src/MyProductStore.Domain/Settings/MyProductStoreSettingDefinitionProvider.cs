using Volo.Abp.Settings;

namespace MyProductStore.Settings;

public class MyProductStoreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MyProductStoreSettings.MySetting1));
    }
}
