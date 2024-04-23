using DorukSoft.OfficialWeb.Domain.Entities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Parola minimum 8 karakter içermelidir.");
        }
    }
}
