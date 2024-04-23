using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, IDTO<List<AppUserListDTO>?>>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IRepository<AppUser> appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<List<AppUserListDTO>?>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var allUsers = await _appUserRepository.GetAllAsync([x => x.AppRole!]);
                if (allUsers != null)
                {
                    return new IDTO<List<AppUserListDTO>?>(200, _mapper.Map<List<AppUserListDTO>>(allUsers), [$"{allUsers.Count} adet kullanıcı bulundu."]);
                }
                return new IDTO<List<AppUserListDTO>?>(404, null, ["Kullanıcı bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<List<AppUserListDTO>?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
