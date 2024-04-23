using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommandRequest, IDTO<AppUserListDTO?>>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMapper _mapper;

        public UpdateAppUserCommandHandler(IRepository<AppUser> appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<AppUserListDTO?>> Handle(UpdateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.GetByIdAsync(request.AppUserId);
                if (user != null)
                {
                    user.Email = request.Email;
                    user.Password = request.Password;
                    user.Name = request.Name;
                    user.Surname = request.Surname;
                    var validator = new AppUserValidator();
                    var result = validator.Validate(user);
                    if (result.IsValid)
                    {
                        user.Password = PasswordHasher.HashPassword(user.Password!);
                        var updatedUser = await _appUserRepository.UpdateAsync(user);
                        return new IDTO<AppUserListDTO?>(200, _mapper.Map<AppUserListDTO>(updatedUser), ["Güncelleme işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<AppUserListDTO?>(300, null, result.Errors.Select(x => x.ErrorMessage).ToList());
                    }
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
