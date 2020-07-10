using FluentValidation;
using ShopApp.WebUI.Models.Accounts;

namespace ShopApp.WebUI.Validators.Accounts
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                               .NotNull().WithMessage("Kullanıcı adı zorunludur.")
                               .Length(2, 15).WithMessage("Kullanıcı adı 5-20 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre zorunludur.")
                               .NotNull().WithMessage("Şifre zorunludur.")
                               .Length(2, 15).WithMessage("Şifre 5-20 karakter uzunluğunda olmalıdır.");
        }
    }
}
