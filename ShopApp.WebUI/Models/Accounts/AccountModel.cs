using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.Accounts
{
    public class AccountModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
