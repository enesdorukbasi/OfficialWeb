using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands
{
    public class DeleteViewerPageMediaCommandRequest : IRequest<IDTO<object?>>
    {
        public int MediaId { get; set; }
    }
}
