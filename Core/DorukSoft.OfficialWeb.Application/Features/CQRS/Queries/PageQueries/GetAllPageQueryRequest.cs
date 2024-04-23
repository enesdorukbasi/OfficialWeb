using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries
{
    public class GetAllPageQueryRequest : IRequest<IDTO<PagesListDTO?>>
    {
    }
}
