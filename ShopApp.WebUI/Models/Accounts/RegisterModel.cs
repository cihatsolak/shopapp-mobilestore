using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.Accounts
{
    public class RegisterModel : AccountModel
    {
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
