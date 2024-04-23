using DorukSoft.OfficialWeb.Domain.PageEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class GenericPageValidator : AbstractValidator<GenericPage>
    {
        public GenericPageValidator()
        {
            RuleFor(x => x.PageTitle).NotNull().NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.PageContent).NotNull().NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.");
            RuleFor(x => x.PageType).NotNull().NotEmpty().WithMessage("Sayfa tipi alanı boş bırakılamaz.");
        }
    }
}
