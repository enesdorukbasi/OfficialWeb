using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BlogQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BlogHandlers
{
    public class GetAllBlogQueryHandler : IRequestHandler<GetAllBlogQueryRequest, IDTO<List<BlogListDTO>?>>
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;

        public GetAllBlogQueryHandler(IRepository<Blog> blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<List<BlogListDTO>?>> Handle(GetAllBlogQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var blogs = await _blogRepository.GetAllAsync();
                return new IDTO<List<BlogListDTO>?>(200, blogs != null && blogs.Count > 0 ? _mapper.Map<List<BlogListDTO>>(blogs) : [], [$"Blog bulundu."]);
            }
            catch (Exception)
            {
                return new IDTO<List<BlogListDTO>?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
