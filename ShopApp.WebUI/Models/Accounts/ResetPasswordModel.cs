using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.Accounts
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
