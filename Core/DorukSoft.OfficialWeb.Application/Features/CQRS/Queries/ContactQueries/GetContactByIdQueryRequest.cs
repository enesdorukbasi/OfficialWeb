using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ContactQueries
{
    public class GetContactByIdQueryRequest : IRequest<IDTO<ContactListDTO?>>
    {
        public int ContactId { get; set; }
        public string? ViewedUserMail { get; set; }
    }
}
