using FluentValidation;
using ShopApp.WebUI.Models.Categories;

namespace ShopApp.WebUI.Validators.Categories
{
    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı zorunludur.")
                                .NotNull().WithMessage("Kategori adı zorunludur.")
                                .Length(2, 15).WithMessage("Kategori adı 2-15 karakter uzunluğunda olmalıdır.");
        }
    }
}
