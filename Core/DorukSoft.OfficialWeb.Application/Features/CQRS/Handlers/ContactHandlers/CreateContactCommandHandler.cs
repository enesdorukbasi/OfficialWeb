using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ContactCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly IEmailService _emailService;

        public CreateContactCommandHandler(IRepository<Contact> contactRepository, IEmailService emailService)
        {
            _contactRepository = contactRepository;
            _emailService = emailService;
        }

        public async Task<IDTO<object?>> Handle(CreateContactCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Contact
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Content = request.Content,
                };
                var validator = new ContactValidator();
                var validation = validator.Validate(entity);
                if (validation.IsValid)
                {
                    var created = await _contactRepository.CreateAsync(entity);
                    if (created != null)
                    {
                        await _emailService.PublishQuee(new EmailSendRequestDTO
                        {
                            ToMailAddress = request.Email!,
                            Subject = "İletişim Formunuz HK.",
                            CcMailAddress = "",
                            Body = $"Sayın {request.FullName},iletişim formunuz başarıyla iletilmiştir. En kısa sürede tarafınıza geri dönüş yapılacaktır."
                        });
                        return new IDTO<object?>(200, null, ["Mesaj başarıyla gönderildi."]);
                    }
                    return new IDTO<object?>(300, null, ["Mesaj gönderilemedi."]);
                }
                return new IDTO<object?>(300, null, validation.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
