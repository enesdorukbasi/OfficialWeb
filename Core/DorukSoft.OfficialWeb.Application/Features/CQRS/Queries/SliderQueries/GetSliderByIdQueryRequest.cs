using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.SliderQueries
{
    public class GetSliderByIdQueryRequest : IRequest<IDTO<SliderListDTO?>>
    {
        public int SliderId { get; set; }
    }
}
