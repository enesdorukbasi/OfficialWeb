using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands
{
    public class UpdateSocialMediaCommandRequest : IRequest<IDTO<object?>>
    {
        public int SocialMediaId { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
    }
}
