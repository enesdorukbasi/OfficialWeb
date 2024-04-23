using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("İçerik alanı boş bırakılamaz.");
            RuleFor(x => x.Keywords).NotNull().NotEmpty().WithMessage("Kelimeler alanı boş bırakılamaz.");
            RuleFor(x => x.IsShowMainPage).NotNull().WithMessage("Ana Sayfa Gösterim alanı boş bırakılamaz.");
            RuleFor(x => x.ImageUrl).NotNull().WithMessage("Görsel alanı boş bırakılamaz.");
        }
    }
}
