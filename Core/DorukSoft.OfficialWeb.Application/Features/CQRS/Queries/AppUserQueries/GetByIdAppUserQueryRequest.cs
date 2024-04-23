using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries
{
    public class GetByIdAppUserQueryRequest : IRequest<IDTO<AppUserListDTO?>>
    {
        public int UserId { get; set; }
    }
}
