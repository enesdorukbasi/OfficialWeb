using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CategoryCommands
{
    public class DeleteCategoryCommandRequest : IRequest<IDTO<object?>>
    {
        public int CategoryId { get; set; }
    }
}
