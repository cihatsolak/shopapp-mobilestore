using FluentValidation;
using ShopApp.WebUI.Models.Accounts;

namespace ShopApp.WebUI.Validators.Accounts
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta adresi zorunludur.")
                                 .NotNull().WithMessage("E-Posta adresi zorunludur.")
                                  .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre zorunludur.")
                                    .NotNull().WithMessage("Şifre zorunludur.");

            RuleFor(x => x.RePassword).Equal(x => x.Password).WithMessage("Parola ve (tekrar) parola birbiriyle eşleşmiyor.");

            RuleFor(x => x.Token).NotEmpty().NotNull();
        }
    }
}
