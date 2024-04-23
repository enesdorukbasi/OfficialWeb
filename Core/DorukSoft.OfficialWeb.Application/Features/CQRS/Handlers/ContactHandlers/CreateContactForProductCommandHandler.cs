using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ContactCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class CreateContactForProductCommandHandler : IRequestHandler<CreateContactForProductCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Contact> _contactRepository;

        public CreateContactForProductCommandHandler(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IDTO<object?>> Handle(CreateContactForProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Contact
                {
                    ProductId = request.ProductId,
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
                        return new IDTO<object?>(200, null, ["Mesaj gönderildi."]);
                    }
                    return new IDTO<object?>(300, null, ["Bir sorun oluştu."]);
                }
                else
                {
                    return new IDTO<object?>(300, null, validation.Errors.Select(x => x.ErrorMessage).ToList());
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
