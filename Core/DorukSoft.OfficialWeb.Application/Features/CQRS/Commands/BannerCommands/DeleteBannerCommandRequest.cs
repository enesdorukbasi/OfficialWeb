using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BannerCommands
{
    public class DeleteBannerCommandRequest : IRequest<IDTO<object?>>
    {
        public int BannerId { get; set; }
    }
}
