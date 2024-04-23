using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries
{
    public class GetAllUsersQueryRequest : IRequest<IDTO<List<AppUserListDTO>?>>
    {
    }
}
