using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries
{
    public class GetAllCategoryQueryRequest : IRequest<IDTO<List<CategoryListDTO>?>>
    {
    }
}
