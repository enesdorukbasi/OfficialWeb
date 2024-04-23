using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class DeleteGenericPageCommandHandler : IRequestHandler<DeleteGenericPageCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<GenericPage> _genericPageRepository;

        public DeleteGenericPageCommandHandler(IRepository<GenericPage> genericPageRepository)
        {
            _genericPageRepository = genericPageRepository;
        }

        public async Task<IDTO<object?>> Handle(DeleteGenericPageCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var genericPage = await _genericPageRepository.GetByIdAsync(request.PageId);
                if (genericPage != null)
                {
                    bool isDeleted = await _genericPageRepository.DeleteAsync(genericPage);
                    if (isDeleted)
                    {
                        return new IDTO<object?>(200, null, ["Bir sorun oluştu."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, ["Bir sorun oluştu."]);
                    }
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Bu data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
