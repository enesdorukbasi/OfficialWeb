using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands
{
    public class UpdateAboutPageCommandRequest : IRequest<IDTO<AboutPageListDTO?>>
    {
        public int AboutId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
