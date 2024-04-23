using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ContactQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQueryRequest, IDTO<List<ContactListDTO>?>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetAllContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<List<ContactListDTO>?>> Handle(GetAllContactQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var contacts = await _contactRepository.GetAllContactsWithIncludes();
                if (contacts != null)
                {
                    return new IDTO<List<ContactListDTO>?>(200, _mapper.Map<List<ContactListDTO>>(contacts), ["İletişimler getirildi."]);
                }
                return new IDTO<List<ContactListDTO>?>(404, null, ["İletişim bulunamadı."]);
            }
            catch (Exception ex)
            {
                return new IDTO<List<ContactListDTO>?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
