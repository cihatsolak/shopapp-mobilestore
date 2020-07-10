using FluentValidation;
using ShopApp.WebUI.Models.Roles;

namespace ShopApp.WebUI.Validators.Roles
{
    public class RoleModelValidator : AbstractValidator<RoleModel>
    {
        public RoleModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Rol adı zorunludur.")
                                .NotNull().WithMessage("Rol adı zorunludur.")
                                .Length(2, 15).WithMessage("Rol adı 5-15 karakter uzunluğunda olmalıdır.");
        }
    }
}
