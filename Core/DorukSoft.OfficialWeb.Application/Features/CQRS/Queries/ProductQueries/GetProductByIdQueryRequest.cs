using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries
{
    public class GetProductByIdQueryRequest : IRequest<IDTO<ProductListDTO?>>
    {
        public int Id { get; set; }
    }
}
