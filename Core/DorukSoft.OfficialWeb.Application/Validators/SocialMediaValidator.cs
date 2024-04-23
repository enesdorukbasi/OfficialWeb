using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class SocialMediaValidator : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidator()
        {
            RuleFor(x=>x.Icon).NotNull().NotEmpty().WithMessage("İkon boş bırakılamaz.");
            RuleFor(x => x.Url).NotNull().NotEmpty().WithMessage("Url boş bırakılamaz.");
        }
    }
}
