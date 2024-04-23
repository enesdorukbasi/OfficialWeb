using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Ad-Soyad zorunludur.");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Email zorunludur.");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage("Telefon numarası zorunludur.");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("Mesaj zorunludur.");
        }
    }
}
