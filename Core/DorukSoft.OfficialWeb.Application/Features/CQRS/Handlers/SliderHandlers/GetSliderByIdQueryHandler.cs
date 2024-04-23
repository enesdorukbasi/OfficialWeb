using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.SliderQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SliderHandlers
{
    public class GetSliderByIdQueryHandler : IRequestHandler<GetSliderByIdQueryRequest, IDTO<SliderListDTO?>>
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IMapper _mapper;

        public GetSliderByIdQueryHandler(IRepository<Slider> sliderRepository, IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<SliderListDTO?>> Handle(GetSliderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _sliderRepository.GetByIdAsync(request.SliderId);
                if (entity != null)
                {
                    return new IDTO<SliderListDTO?>(200, _mapper.Map<SliderListDTO>(entity), ["Data bulundu."]);
                }
                return new IDTO<SliderListDTO?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<SliderListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
