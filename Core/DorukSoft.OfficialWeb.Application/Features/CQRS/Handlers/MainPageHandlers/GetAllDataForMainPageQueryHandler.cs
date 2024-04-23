using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.MainPage;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.MainPageHandlers
{
    public class GetAllDataForMainPageQueryHandler : IRequestHandler<GetAllDataForMainPageQueryRequest, IDTO<MainPageListDTO?>>
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IRepository<Banner> _bannerRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SocialMedia> _socialMediaRepository;
        private readonly IRepository<GenericPage> _genericPageRepository;
        private readonly IRepository<AboutPage> _aboutPageRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<CompanyInformation> _companyInformationRepository;
        private readonly IMapper _mapper;

        public GetAllDataForMainPageQueryHandler(IRepository<Slider> sliderRepository, IRepository<Banner> bannerRepository, IRepository<Category> categoryRepository, IMapper mapper, IRepository<SocialMedia> socialMediaRepository, IRepository<GenericPage> genericPageRepository, IRepository<AboutPage> aboutPageRepository, IRepository<Blog> blogRepository, IRepository<CompanyInformation> companyInformationRepository)
        {
            _socialMediaRepository = socialMediaRepository;
            _genericPageRepository = genericPageRepository;
            _aboutPageRepository = aboutPageRepository;
            _sliderRepository = sliderRepository;
            _bannerRepository = bannerRepository;
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
            _mapper = mapper;
            _companyInformationRepository = companyInformationRepository;
        }

        public async Task<IDTO<MainPageListDTO?>> Handle(GetAllDataForMainPageQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var sliders = await _sliderRepository.GetAllAsync();
                var categories = await _categoryRepository.GetAllByFilterAsync(x => x.IsShowMainPage == true);
                var banner = await _bannerRepository.GetByFilterAsync(x => x.IsShowMainPage);
                var socialMedias = await _socialMediaRepository.GetAllAsync();
                var aboutPage = await _aboutPageRepository.GetAllAsync();
                var genericPage = await _genericPageRepository.GetAllAsync();
                var blogs = await _blogRepository.GetAllByFilterAsync(x => x.IsShowMainPage);
                var companyInfos = await _companyInformationRepository.GetAllAsync();
                if (sliders != null && categories != null && banner != null && aboutPage != null && genericPage != null && blogs != null)
                {
                    return new IDTO<MainPageListDTO?>(200,
                        new MainPageListDTO
                        {
                            AboutPage = _mapper.Map<AboutPageListDTO>(aboutPage.First()),
                            GenericPages = genericPage.Count > 0 ? _mapper.Map<List<GenericPageListDTO>>(genericPage) : [],
                            Sliders = sliders.Count > 0 ? _mapper.Map<List<SliderListDTO>>(sliders) : [],
                            Categories = categories.Count > 0 ? _mapper.Map<List<CategoryListDTO>>(categories) : [],
                            Banner = _mapper.Map<BannerListDTO>(banner),
                            Blogs = _mapper.Map<List<BlogListDTO>>(blogs),
                            CompanyInformation = _mapper.Map<CompanyInformationListDTO>(companyInfos.First()),
                            SocialMediaLists = socialMedias.Count > 0 ? _mapper.Map<List<SocialMediaListDTO>>(socialMedias) : [],
                        }
                        , ["Bulundu."]) ;
                }
                return new IDTO<MainPageListDTO?>(404, null, ["Bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<MainPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
