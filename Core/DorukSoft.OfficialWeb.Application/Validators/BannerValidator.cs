using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class BannerValidator : AbstractValidator<Banner>
    {
        public BannerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.IsShowMainPage).NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.IsShowAboutPage).NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
        }
    }
}
