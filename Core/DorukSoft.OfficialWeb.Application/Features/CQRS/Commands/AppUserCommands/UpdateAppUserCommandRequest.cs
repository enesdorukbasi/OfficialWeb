using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands
{
    public class UpdateAppUserCommandRequest : IRequest<IDTO<AppUserListDTO?>>
    {
        public int AppUserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
