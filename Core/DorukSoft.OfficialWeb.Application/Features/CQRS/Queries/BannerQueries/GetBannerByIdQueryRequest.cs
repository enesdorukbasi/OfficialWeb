using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BannerQueries
{
    public class GetBannerByIdQueryRequest : IRequest<IDTO<BannerListDTO?>>
    {
        public int BannerId { get; set; }
    }
}
