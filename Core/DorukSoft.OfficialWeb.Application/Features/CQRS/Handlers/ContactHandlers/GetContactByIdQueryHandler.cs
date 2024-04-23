using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ContactQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQueryRequest, IDTO<ContactListDTO?>>
    {
        private readonly IContactRepository _contactRepositorySpecial;
        private readonly IRepository<Contact> _contactRepository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IContactRepository contactRepositorySpecial, IRepository<Contact> contactRepository, IMapper mapper)
        {
            _contactRepositorySpecial = contactRepositorySpecial;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<ContactListDTO?>> Handle(GetContactByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _contactRepositorySpecial.GetByIdContactsWithIncludes(request.ContactId);
                if (entity != null)
                {
                    if (request.ViewedUserMail != null)
                    {
                        if (entity.ShowedAdminMails != null)
                        {
                            if (entity.ShowedAdminMails.Contains(request.ViewedUserMail) == false)
                            {
                                entity.ShowedAdminMails.Add(request.ViewedUserMail);
                                var updated = await _contactRepository.UpdateAsync(entity);
                                var entityUpdated = await _contactRepositorySpecial.GetByIdContactsWithIncludes(updated.ContactId);
                                if (entityUpdated != null)
                                {
                                    return new IDTO<ContactListDTO?>(200, _mapper.Map<ContactListDTO>(entityUpdated), ["İletişim bulundu."]);
                                }
                            }
                        }
                        else
                        {
                            entity.ShowedAdminMails = new List<string> { request.ViewedUserMail };
                            var updated = await _contactRepository.UpdateAsync(entity);
                            if (updated != null)
                            {
                                return new IDTO<ContactListDTO?>(200, _mapper.Map<ContactListDTO>(updated), ["İletişim bulundu."]);
                            }
                        }
                    }
                    return new IDTO<ContactListDTO?>(200, _mapper.Map<ContactListDTO>(entity), ["İletişim bulundu."]);
                }
                return new IDTO<ContactListDTO?>(404, null, ["İletişim bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<ContactListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
