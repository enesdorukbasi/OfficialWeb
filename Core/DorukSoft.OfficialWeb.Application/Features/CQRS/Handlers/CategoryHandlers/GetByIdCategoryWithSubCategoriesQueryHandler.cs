using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CategoryHandlers
{
    public class GetByIdCategoryWithSubCategoriesQueryHandler : IRequestHandler<GetByIdCategoryWithSubCategoriesQueryRequest, IDTO<CategoryListDTO?>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetByIdCategoryWithSubCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<CategoryListDTO?>> Handle(GetByIdCategoryWithSubCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _categoryRepository.GetByIdWithSubCategories(request.CategoryId);
                if(response != null)
                {
                    return new IDTO<CategoryListDTO?>(200, _mapper.Map<CategoryListDTO>(response), ["Data bulundu."]);
                }
                return new IDTO<CategoryListDTO?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<CategoryListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
 