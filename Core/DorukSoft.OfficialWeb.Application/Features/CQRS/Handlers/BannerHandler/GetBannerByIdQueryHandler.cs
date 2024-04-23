using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BannerQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BannerHandler
{
    public class GetBannerByIdQueryHandler : IRequestHandler<GetBannerByIdQueryRequest, IDTO<BannerListDTO?>>
    {
        private readonly IRepository<Banner> _bannerRepository;
        private readonly IMapper _mapper;

        public GetBannerByIdQueryHandler(IRepository<Banner> bannerRepository, IMapper mapper)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<BannerListDTO?>> Handle(GetBannerByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _bannerRepository.GetByIdAsync(request.BannerId);
                if(entity != null)
                {
                    return new IDTO<BannerListDTO?>(200, _mapper.Map<BannerListDTO>(entity), ["Data bulundu."]);
                }
                return new IDTO<BannerListDTO?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<BannerListDTO?>(500,null, ["Bir sorun oluştu."]);
            }
        }
    }
}
