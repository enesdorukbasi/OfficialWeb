using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands
{
    public class DeleteBlogCommandRequest : IRequest<IDTO<object?>>
    {
        public int BlogId { get; set; }
    }
}
