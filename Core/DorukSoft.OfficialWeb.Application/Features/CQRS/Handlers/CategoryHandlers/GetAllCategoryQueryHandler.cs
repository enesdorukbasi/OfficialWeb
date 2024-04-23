using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CategoryHandlers
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, IDTO<List<CategoryListDTO>?>>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mappper;

        public GetAllCategoryQueryHandler(IRepository<Category> categoryRepository, IMapper mappper)
        {
            _categoryRepository = categoryRepository;
            _mappper = mappper;
        }

        public async Task<IDTO<List<CategoryListDTO>?>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _categoryRepository.GetAllByFilterAsync(x => x.ParentCategoryId == null, [x => x.SubCategories!, x => x.ParentCategory!]);
                if (categories != null)
                {
                    return new IDTO<List<CategoryListDTO>?>(200, _mappper.Map<List<CategoryListDTO>>(categories), [$"{categories.Count} adet kategori bulundu."]);
                }
                return new IDTO<List<CategoryListDTO>?>(200, null, ["Kategori bulunmuyor."]);
            }
            catch (Exception)
            {
                return new IDTO<List<CategoryListDTO>?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
