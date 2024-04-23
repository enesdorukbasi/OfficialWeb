using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorukSoft.OfficialWeb.Application.Validators
{
    public class CompanyInformationValidator : AbstractValidator<CompanyInformation>
    {
        public CompanyInformationValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().NotNull().WithMessage("Şirket adı boş bırakılamaz.");
            RuleFor(x => x.CompanyTitle).NotEmpty().NotNull().WithMessage("Şirket başlığı boş bırakılamaz.");
            RuleFor(x => x.WhatsappPhoneNumber).NotEmpty().NotNull().WithMessage("Whatsapp numarası boş bırakılamaz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("Telefon numarası boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email boş bırakılamaz.");
            RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("Adres adı boş bırakılamaz.");
        }
    }
}
