using ShopApp.DataAccess.Identity;
using ShopApp.WebUI.Models.Roles;
using System.Collections.Generic;

namespace ShopApp.WebUI.Factories
{
    public partial interface IRoleModelFactory
    {
        List<RoleModel> PrepareRoleModelList(List<ApplicationRole> applicationRoles = null);
        ApplicationRole PrepareApplicationRole(RoleModel roleModel = null);
        RoleModel PrepareRoleModel(ApplicationRole applicationRole = null);
    }
}
