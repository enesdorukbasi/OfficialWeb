using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public RegisterAppUserCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<IDTO<object?>> Handle(RegisterAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new AppUser()
                {
                    Email = request.Email,
                    Password = request.Password,
                    Name = request.Name,
                    Surname = request.Surname,
                    IsActive = true,
                    AppRoleId = 2,
                };
                var validator = new AppUserValidator();
                var result = validator.Validate(user);
                if (result.IsValid)
                {
                    user.Password = PasswordHasher.HashPassword(user.Password);
                    var createdUser = await _appUserRepository.CreateAsync(user);
                    return new IDTO<object?>(200, null, ["Ekleme işlemi başarılı."]);
                }
                else
                {
                    return new IDTO<object?>(300, null, result.Errors.Select(x => x.ErrorMessage).ToList());
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
