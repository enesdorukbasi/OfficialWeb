using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ProductCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.ProductHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Product> _productRepository;

        public DeleteProductCommandHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IDTO<object?>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _productRepository.GetByIdAsync(request.Id);
                if(entity != null)
                {
                    var isDeleted = await _productRepository.DeleteAsync(entity);
                    if (isDeleted)
                    {
                        return new IDTO<object?>(200, null, ["Silme işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, ["Bir sorun oluştu."]);
                    }
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Ürün bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
