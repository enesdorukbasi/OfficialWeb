using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.SettingsPageQueries
{
    public class GetSettingsPageQueryRequest : IRequest<IDTO<SettingsPageListDTO?>>
    {
    }
}
