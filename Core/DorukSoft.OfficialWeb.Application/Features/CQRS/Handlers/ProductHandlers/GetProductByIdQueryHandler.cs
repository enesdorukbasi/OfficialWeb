using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ProductHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, IDTO<ProductListDTO?>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<ProductListDTO?>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id, [x => x.Category!]);
                if (product != null)
                {
                    return new IDTO<ProductListDTO?>(200, _mapper.Map<ProductListDTO>(product), ["Bir sorun oluştu."]);
                }
                else
                {
                    return new IDTO<ProductListDTO?>(404, null, ["Product bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<ProductListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
