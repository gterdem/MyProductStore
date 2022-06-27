using MyProductStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MyProductStore.Permissions;

public class MyProductStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MyProductStorePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MyProductStorePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MyProductStoreResource>(name);
    }
}
