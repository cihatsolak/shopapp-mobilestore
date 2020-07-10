using FluentValidation;
using ShopApp.WebUI.Models.Accounts;

namespace ShopApp.WebUI.Validators.Accounts
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad ve soyad zorunludur.")
                               .NotNull().WithMessage("Ad ve soyad zorunludur.")
                               .Length(5, 30).WithMessage("Ad ve soyadınız 5-30 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                               .NotNull().WithMessage("Kullanıcı adı zorunludur.")
                               .Length(2, 15).WithMessage("Kullanıcı adı 5-20 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre zorunludur.")
                               .NotNull().WithMessage("Şifre zorunludur.")
                               .Length(2, 15).WithMessage("Şifre 5-20 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.RePassword).Equal(x => x.Password).WithMessage("Parola ve (tekrar) parola birbiriyle eşleşmiyor.");

            RuleFor(x => x.RePassword).NotEmpty().WithMessage("(Tekrar) Şifre zorunludur.")
                               .NotNull().WithMessage("(Tekrar) Şifre zorunludur.")
                               .Length(2, 15).WithMessage("(Tekrar) Şifre 5-20 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta adresi zorunludur.")
                                 .NotNull().WithMessage("E-Posta adresi zorunludur.")
                                  .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");
        }
    }
}
