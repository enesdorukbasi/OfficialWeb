using DorukSoft.OfficialWeb.Domain.PageEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class AboutPageValidator : AbstractValidator<AboutPage>
    {
        public AboutPageValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("Açıklama alanı boş bırakılamaz.");
        }
    }
}
