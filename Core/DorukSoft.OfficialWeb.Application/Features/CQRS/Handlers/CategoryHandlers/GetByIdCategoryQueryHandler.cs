using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CategoryHandlers
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQueryRequest, IDTO<CategoryListDTO?>>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetByIdCategoryQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<CategoryListDTO?>> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId, [x => x.ParentCategory!]);
                if (category != null)
                {
                    return new IDTO<CategoryListDTO?>(200, _mapper.Map<CategoryListDTO>(category), ["Kategori bulundu."]);
                }
                return new IDTO<CategoryListDTO?>(404, null, ["Bu id'de kategori bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<CategoryListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
