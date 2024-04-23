using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class GetAboutPageQueryHandler : IRequestHandler<GetAboutPageQueryRequest, IDTO<AboutPageListDTO?>>
    {
        private readonly IRepository<AboutPage> _aboutPageRepository;
        private readonly IRepository<Banner> _bannerRepository;
        private readonly IMapper _mapper;
        public GetAboutPageQueryHandler(IRepository<AboutPage> aboutPageRepository, IMapper mapper, IRepository<Banner> bannerRepository)
        {
            _aboutPageRepository = aboutPageRepository;
            _mapper = mapper;
            _bannerRepository = bannerRepository;
        }

        public async Task<IDTO<AboutPageListDTO?>> Handle(GetAboutPageQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _aboutPageRepository.GetAllAsync();
                var banner = await _bannerRepository.GetByFilterAsync(x => x.IsShowAboutPage);
                if (response != null && banner != null)
                {
                    var aboutPageDTO = new AboutPageListDTO
                    {
                        Banner = _mapper.Map<BannerListDTO>(banner),
                        Title = response.First().Title,
                        Content = response.First().Content,
                    };
                    return new IDTO<AboutPageListDTO?>(200, aboutPageDTO, ["Data bulundu."]);
                }
                else
                {
                    return new IDTO<AboutPageListDTO?>(404, null, ["Data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<AboutPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
