using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands
{
    public class ChangeStatusAppUserCommandRequest : IRequest<IDTO<object?>>
    {
        public int UserId { get; set; }
    }
}
