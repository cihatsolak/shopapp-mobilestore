using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var email = configuration["Data:AdminUser:email"];
            var password = configuration["Data:AdminUser:password"];
            string role = configuration["Data:AdminUser:role"];
            var fullname = configuration["Data:AdminUser:fullname"];

            var applicationUser = await userManager.FindByNameAsync(username);

            if (applicationUser == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role));

                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    FullName = fullname,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
