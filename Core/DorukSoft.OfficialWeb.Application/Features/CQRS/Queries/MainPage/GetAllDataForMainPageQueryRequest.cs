using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.MainPage
{
    public class GetAllDataForMainPageQueryRequest : IRequest<IDTO<MainPageListDTO?>>
    {
    }
}
