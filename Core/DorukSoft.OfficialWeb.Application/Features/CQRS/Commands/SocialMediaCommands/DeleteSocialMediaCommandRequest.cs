using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands
{
    public class DeleteSocialMediaCommandRequest : IRequest<IDTO<object?>>
    {
        public int SocialMediaId { get; set; }
    }
}
