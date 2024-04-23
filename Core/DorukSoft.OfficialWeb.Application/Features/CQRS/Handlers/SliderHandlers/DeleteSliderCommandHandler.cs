using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SliderHandlers
{
    public class DeleteSliderCommandHandler : IRequestHandler<DeleteSliderCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly FileUploader _fileUploader;

        public DeleteSliderCommandHandler(IRepository<Slider> sliderRepository, FileUploader fileUploader)
        {
            _sliderRepository = sliderRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(DeleteSliderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _sliderRepository.GetByIdAsync(request.SliderId);
                if (entity != null)
                {
                    bool isDeleted = await _sliderRepository.DeleteAsync(entity);
                    if (isDeleted)
                    {
                        _fileUploader.DeleteFile(entity.SliderImageUrl!);
                        return new IDTO<object?>(200, null, ["Silme işlemi başarılı."]);
                    }
                    return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Data bulunamadı."]);
                }
            }
            catch(Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
