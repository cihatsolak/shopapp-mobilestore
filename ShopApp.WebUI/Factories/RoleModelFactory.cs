using ShopApp.DataAccess.Identity;
using ShopApp.WebUI.Models.Roles;
using System;
using System.Collections.Generic;

namespace ShopApp.WebUI.Factories
{
    public class RoleModelFactory : IRoleModelFactory
    {
        public virtual ApplicationRole PrepareApplicationRole(RoleModel roleModel = null)
        {
            if (roleModel == null)
                throw new ArgumentNullException(nameof(roleModel));

            return new ApplicationRole(roleModel.Name);
        }

        public RoleModel PrepareRoleModel(ApplicationRole applicationRole = null)
        {
            if (applicationRole == null)
                throw new ArgumentNullException(nameof(applicationRole));

            return new RoleModel
            {
                Id = applicationRole.Id,
                Name = applicationRole.Name
            };
        }

        public virtual List<RoleModel> PrepareRoleModelList(List<ApplicationRole> applicationRoles = null)
        {
            if (applicationRoles == null)
                throw new ArgumentNullException(nameof(applicationRoles));

            var roles = new List<RoleModel>();

            foreach (var appRole in applicationRoles)
            {
                roles.Add(new RoleModel
                {
                    Id = appRole.Id,
                    Name = appRole.Name
                });
            }

            return roles;
        }
    }
}
