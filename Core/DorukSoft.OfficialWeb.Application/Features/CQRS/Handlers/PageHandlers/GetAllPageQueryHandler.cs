using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class GetAllPageQueryHandler : IRequestHandler<GetAllPageQueryRequest, IDTO<PagesListDTO?>>
    {
        private readonly IRepository<AboutPage> _aboutPageRepository;
        private readonly IRepository<GenericPage> _genericPageRepository;
        private readonly IMapper _mapper;

        public GetAllPageQueryHandler(IRepository<AboutPage> aboutPageRepository, IRepository<GenericPage> genericPageRepository, IMapper mapper)
        {
            _aboutPageRepository = aboutPageRepository;
            _genericPageRepository = genericPageRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<PagesListDTO?>> Handle(GetAllPageQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var aboutPageList = await _aboutPageRepository.GetAllAsync();
                var genericPageList = await _genericPageRepository.GetAllAsync();
                var pagesListDTO = new PagesListDTO
                {
                    AboutPage = _mapper.Map<AboutPageListDTO>(aboutPageList.First()),
                    GenericPages = _mapper.Map<List<GenericPageListDTO>>(genericPageList),
                };
                return new IDTO<PagesListDTO?>(200, pagesListDTO, ["Tüm sayfalar getirildi."]);
            }
            catch (Exception)
            {
                return new IDTO<PagesListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
