using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries
{
    public class GetAllProductByCategoryIdQueryRequest : IRequest<IDTO<List<ProductListDTO>?>>
    {
        public int CategoryId { get; set; }
    }
}
