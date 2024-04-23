using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands
{
    public class CreateSocialMediaCommandRequest : IRequest<IDTO<object?>>
    {
        public string? Url { get; set; }
        public string? Icon { get; set; }
    }
}
