using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class GetByIdAppUserQueryHandler : IRequestHandler<GetByIdAppUserQueryRequest, IDTO<AppUserListDTO?>>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public GetByIdAppUserQueryHandler(IRepository<AppUser> appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<AppUserListDTO?>> Handle(GetByIdAppUserQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.GetByIdAsync(request.UserId);
                if (user != null)
                {
                    return new IDTO<AppUserListDTO?>(200, _mapper.Map<AppUserListDTO>(user), ["Kullanıcı bulundu."]);
                }
                return new IDTO<AppUserListDTO?>(404, null, ["Kullanıcı bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<AppUserListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
