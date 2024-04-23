using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands
{
    public class DeleteGenericPageCommandRequest : IRequest<IDTO<object?>>
    {
        public int PageId { get; set; }
    }
}
