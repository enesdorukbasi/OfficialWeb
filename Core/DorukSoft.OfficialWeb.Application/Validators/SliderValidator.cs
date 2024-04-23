using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using FluentValidation;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class SliderValidator : AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(x=>x.SliderContent).MinimumLength(0).NotNull().WithMessage("Açıklama kısmı boş bırakılamaz.");
            RuleFor(x => x.SliderImageUrl).NotNull().WithMessage("Görsel kısmı boş bırakılamaz.");
        }
    }
}
