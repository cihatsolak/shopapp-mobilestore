using FluentValidation;
using ShopApp.WebUI.Models.Products;

namespace ShopApp.WebUI.Validators.Products
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı zorunludur.")
                                .NotNull().WithMessage("Ürün adı zorunludur.")
                                .Length(2, 50).WithMessage("Ürün adı 2-50 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün açıklaması zorunludur.")
                                        .NotNull().WithMessage("Ürün açıklaması zorunludur.")
                                        .Length(2, 50).WithMessage("Ürün adı 25-250 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün açıklaması zorunludur.")
                                .NotNull().WithMessage("Ürün açıklaması zorunludur.")
                                .NotEqual(0).WithMessage("Ürün fiyatı zorunludur.");
        }
    }
}
