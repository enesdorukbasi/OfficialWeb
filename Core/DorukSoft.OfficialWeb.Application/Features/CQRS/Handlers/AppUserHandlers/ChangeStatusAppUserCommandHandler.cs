using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class ChangeStatusAppUserCommandHandler : IRequestHandler<ChangeStatusAppUserCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public ChangeStatusAppUserCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<IDTO<object?>> Handle(ChangeStatusAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.GetByIdAsync(request.UserId);
                if (user != null)
                {
                    user.IsActive = !user.IsActive;
                    var changedUser = await _appUserRepository.UpdateAsync(user);
                    if (changedUser != null)
                    {
                        return new IDTO<object?>(200, null, ["Güncelleme işlemi başarılı."]);
                    }
                    return new IDTO<object?>(300, null, ["Bir sorun oluştu."]);
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Kullanıcı bulunamadı."]);
                }                
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
            throw new NotImplementedException();
        }
    }
}
