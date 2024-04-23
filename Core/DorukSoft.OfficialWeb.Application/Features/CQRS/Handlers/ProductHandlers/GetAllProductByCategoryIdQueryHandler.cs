using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ProductHandlers
{
    public class GetAllProductByCategoryIdQueryHandler : IRequestHandler<GetAllProductByCategoryIdQueryRequest, IDTO<List<ProductListDTO>?>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductByCategoryIdQueryHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<List<ProductListDTO>?>> Handle(GetAllProductByCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetAllByFilterAsync(x => x.CategoryId == request.CategoryId);
                if (products != null)
                {
                    return new IDTO<List<ProductListDTO>?>(200, products.Count > 0 ? _mapper.Map<List<ProductListDTO>?>(products) : [], ["Data bulundu."]);
                }
                return new IDTO<List<ProductListDTO>?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<List<ProductListDTO>?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
