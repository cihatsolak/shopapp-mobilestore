namespace ShopApp.WebUI.Models.Accounts
{
    public class LoginModel : AccountModel
    {
        public bool RememberMe { get; set; }
        public string RedirectUrl { get; set; } = "~/Home/Index";
    }
}
