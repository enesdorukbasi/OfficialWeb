using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BlogQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BlogHandlers
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQueryRequest, IDTO<BlogListDTO?>>
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;

        public GetBlogByIdQueryHandler(IRepository<Blog> blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<BlogListDTO?>> Handle(GetBlogByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _blogRepository.GetByIdAsync(request.BlogId);
                if (entity != null)
                {
                    return new IDTO<BlogListDTO?>(200, _mapper.Map<BlogListDTO>(entity), ["Blog bulundu."]);
                }
                return new IDTO<BlogListDTO?>(404, null, ["Blog bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<BlogListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
