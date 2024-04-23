using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries
{
    public class GetByIdCategoryWithSubCategoriesQueryRequest : IRequest<IDTO<CategoryListDTO?>>
    {
        public int CategoryId { get; set; }
    }
}
