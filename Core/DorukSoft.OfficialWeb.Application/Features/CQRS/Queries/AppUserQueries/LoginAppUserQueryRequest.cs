using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries
{
    public class LoginAppUserQueryRequest : IRequest<IDTO<AppUserLoginDTO?>>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
