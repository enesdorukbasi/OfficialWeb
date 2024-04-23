using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ContactCommands
{
    public class CreateContactForProductCommandRequest : IRequest<IDTO<object?>>
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Content { get; set; }
    }
}
