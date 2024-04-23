using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Ad alanı boş bırakılamaz.");
            RuleFor(x => x.Keywords).NotEmpty().NotNull().WithMessage("Kelimeler alanı boş bırakılamaz.");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("Açıklama alanı boş bırakılamaz.");
            //RuleFor(x => x.IsShowMainPage).NotEmpty().NotNull().WithMessage("Ana sayfa gösterim alanı boş bırakılamaz.");
        }
    }
}
