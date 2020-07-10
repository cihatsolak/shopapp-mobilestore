using ShopApp.DataAccess.Identity;
using ShopApp.WebUI.Models.Accounts;

namespace ShopApp.WebUI.Factories
{
    public class AccountModelFactory : IAccountModelFactory
    {
        public virtual ApplicationUser PrepareApplicationUserEntity(RegisterModel registerModel = null)
        {
            var applicationUser = new ApplicationUser();

            if (registerModel != null)
            {
                applicationUser.UserName = registerModel.Username;
                applicationUser.Email = registerModel.Email;
                applicationUser.FullName = registerModel.FullName;

                return applicationUser;
            }

            return null;
        }
    }
}
