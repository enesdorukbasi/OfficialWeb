using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.Entities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.AppUserHandlers
{
    public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<AppUser> _appUserRepository;

        public DeleteAppUserCommandHandler(IRepository<AppUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<IDTO<object?>> Handle(DeleteAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _appUserRepository.GetByIdAsync(request.UserId);
                if (user != null)
                {
                    bool isDeleted = await _appUserRepository.DeleteAsync(user);
                    if (isDeleted)
                    {
                        return new IDTO<object?>(200, null, ["Silme işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, ["Silme işlemi başarısız."]);
                    }
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Bu kullanıcı bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
