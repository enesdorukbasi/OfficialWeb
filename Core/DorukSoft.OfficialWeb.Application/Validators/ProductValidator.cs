using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Başlık alanı boş bırakılamaz.");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("Açıklama alanı boş bırakılamaz.");
            RuleFor(x => x.Keywords).NotEmpty().NotNull().WithMessage("Kelimeler alanı boş bırakılamaz.");
            RuleFor(x => x.Price).NotNull().WithMessage("Fiyat alanı boş bırakılamaz.");
            RuleFor(x => x.Tax).NotNull().WithMessage("Vergi alanı boş bırakılamaz.");
            RuleFor(x => x.Discount).NotNull().WithMessage("İndirim alanı boş bırakılamaz.");
            RuleFor(x => x.Quantity).NotNull().WithMessage("Adet alanı boş bırakılamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().NotNull().WithMessage("Kategori alanı boş bırakılamaz.");
        }
    }
}
