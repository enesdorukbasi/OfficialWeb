using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ProductCommands
{
    public class DeleteProductCommandRequest : IRequest<IDTO<object?>>
    {
        public int Id { get; set; }
    }
}
