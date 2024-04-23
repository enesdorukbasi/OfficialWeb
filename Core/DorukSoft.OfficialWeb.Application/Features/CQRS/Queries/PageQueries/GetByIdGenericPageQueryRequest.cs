using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries
{
    public class GetByIdGenericPageQueryRequest : IRequest<IDTO<GenericPageListDTO?>>
    {
        public int PageId { get; set; }
    }
}
