using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BlogQueries
{
    public class GetAllBlogQueryRequest : IRequest<IDTO<List<BlogListDTO>?>>
    {
    }
}
