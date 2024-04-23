using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class DeleteViewerPageMediaCommandHandler : IRequestHandler<DeleteViewerPageMediaCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<ViewerPageMedia> _viewerPageMediaRepository;

        public DeleteViewerPageMediaCommandHandler(IRepository<ViewerPageMedia> viewerPageMediaRepository)
        {
            _viewerPageMediaRepository = viewerPageMediaRepository;
        }

        public async Task<IDTO<object?>> Handle(DeleteViewerPageMediaCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity =await _viewerPageMediaRepository.GetByIdAsync(request.MediaId);
                if(entity != null)
                {
                    bool isDeleted = await _viewerPageMediaRepository.DeleteAsync(entity);
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
                    return new IDTO<object?>(404, null, ["Data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
