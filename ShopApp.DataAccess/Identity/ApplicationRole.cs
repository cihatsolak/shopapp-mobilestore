using Microsoft.AspNetCore.Identity;

namespace ShopApp.DataAccess.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name) : base(name) { Name = name; }
    }
}
