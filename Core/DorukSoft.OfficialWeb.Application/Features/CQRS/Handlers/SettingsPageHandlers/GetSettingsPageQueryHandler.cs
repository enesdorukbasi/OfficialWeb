using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.SettingsPageQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SettingsPageHandlers
{
    public class GetSettingsPageQueryHandler : IRequestHandler<GetSettingsPageQueryRequest, IDTO<SettingsPageListDTO?>>
    {
        private readonly IRepository<CompanyInformation> _companyInformationRepository;
        private readonly IRepository<SocialMedia> _socialMediaRepository;
        private readonly IRepository<Banner> _bannerRepository;
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IMapper _mapper;

        public GetSettingsPageQueryHandler(IRepository<CompanyInformation> companyInformationRepository, IRepository<SocialMedia> socialMediaRepository, IRepository<Banner> bannerRepository, IRepository<Slider> sliderRepository, IMapper mapper)
        {
            _companyInformationRepository = companyInformationRepository;
            _socialMediaRepository = socialMediaRepository;
            _bannerRepository = bannerRepository;
            _sliderRepository = sliderRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<SettingsPageListDTO?>> Handle(GetSettingsPageQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var companyInformation = await _companyInformationRepository.GetAllAsync();
                var socialMedias = await _socialMediaRepository.GetAllAsync();
                var banners = await _bannerRepository.GetAllAsync();
                var sliders = await _sliderRepository.GetAllAsync();
                if (companyInformation != null && socialMedias != null && banners != null && sliders != null)
                {
                    return new IDTO<SettingsPageListDTO?>(
                        200,
                        new SettingsPageListDTO
                        {
                            Banners = _mapper.Map<List<BannerListDTO>>(banners),
                            CompanyInformation = companyInformation.Count > 0 ? _mapper.Map<CompanyInformationListDTO>(companyInformation.First()) : null,
                            Sliders = _mapper.Map<List<SliderListDTO>>(sliders),
                            SocialMedias = _mapper.Map<List<SocialMediaListDTO>>(socialMedias),
                        },
                        ["Bir sorun oluştu."]
                        );
                }
                else
                {
                    return new IDTO<SettingsPageListDTO?>(404, null, ["Data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<SettingsPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
