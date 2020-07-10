using ShopApp.DataAccess.Identity;
using ShopApp.WebUI.Models.Accounts;

namespace ShopApp.WebUI.Factories
{
    public partial interface IAccountModelFactory
    {
        ApplicationUser PrepareApplicationUserEntity(RegisterModel registerModel = null);
    }
}
