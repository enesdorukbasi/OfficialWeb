using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BlogQueries
{
    public class GetBlogByIdQueryRequest : IRequest<IDTO<BlogListDTO?>>
    {
        public int BlogId { get; set; }
    }
}
