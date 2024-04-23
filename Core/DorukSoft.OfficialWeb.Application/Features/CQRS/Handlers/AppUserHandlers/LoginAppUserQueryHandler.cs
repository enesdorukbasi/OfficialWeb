using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class LoginAppUserQueryHandler : IRequestHandler<LoginAppUserQueryRequest, IDTO<AppUserLoginDTO?>>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public LoginAppUserQueryHandler(IAppUserRepository appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<AppUserLoginDTO?>> Handle(LoginAppUserQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.LoginAsync(request.Email, request.Password);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        var dto = JwtTokenProcesses.GenerateToken(_mapper.Map<AppUserLoginDTO>(user));
                        return new IDTO<AppUserLoginDTO?>(200, dto, ["Giriş işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<AppUserLoginDTO?>(300, null, ["Hesap kullanıma kapatılmıştır."]);
                    }
                }
                return new IDTO<AppUserLoginDTO?>(404, null, ["Email ve ya şifre hatalı."]);
            }
            catch (Exception)
            {
                return new IDTO<AppUserLoginDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}

