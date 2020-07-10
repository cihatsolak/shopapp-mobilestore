using FluentValidation;
using ShopApp.WebUI.Models.Orders;

namespace ShopApp.WebUI.Validators.Orders
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Lütfen adınızı giriniz.")
                                .NotNull().WithMessage("Lütfen adınızı giriniz.")
                                .Length(3, 25).WithMessage("Adınız 3-25 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.")
                                .NotNull().WithMessage("Lütfen soyadınızı giriniz.")
                                .Length(2, 25).WithMessage("Soyadınız 2-25 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Lütfen geçerli bir adres giriniz.")
                                   .NotNull().WithMessage("Lütfen geçerli bir adres giriniz.")
                                   .Length(10, 250).WithMessage("Lütfen geçerli bir adres giriniz.");

            RuleFor(x => x.CityId).NotEqual(0).WithMessage("Lütfen şehrinizi seçiniz.");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Lütfen telefon numaranızı giriniz.")
                                 .NotNull().WithMessage("Lütfen telefon numaranızı giriniz.")
                                 .Length(10, 12).WithMessage("Lütfen telefon numaranızı giriniz.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen e-posta adresinizi giriniz.")
                                 .NotNull().WithMessage("Lütfen e-posta adresinizi giriniz.")
                                 .EmailAddress().WithMessage("Lütfen geçerli bir posta adresi giriniz.");

        }
    }
}
