using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ContactQueries
{
    public class GetAllContactQueryRequest : IRequest<IDTO<List<ContactListDTO>?>>
    {
    }
}
