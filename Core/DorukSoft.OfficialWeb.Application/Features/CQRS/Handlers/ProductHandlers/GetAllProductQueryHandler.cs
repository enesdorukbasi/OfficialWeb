using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ProductHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IDTO<List<ProductListDTO>?>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<List<ProductListDTO>?>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.GetAllAsync([x => x.Category!]);
                if (products != null)
                {
                    return new IDTO<List<ProductListDTO>?>(200, _mapper.Map<List<ProductListDTO>>(products), ["Bir sorun oluştu."]);
                }
                else
                {
                    return new IDTO<List<ProductListDTO>?>(404, null, ["Product bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<List<ProductListDTO>?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
