using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SliderHandlers
{
    public class UpdateSliderCommandHandler : IRequestHandler<UpdateSliderCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly FileUploader _fileUploader;

        public UpdateSliderCommandHandler(IRepository<Slider> sliderRepository, FileUploader fileUploader)
        {
            _sliderRepository = sliderRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(UpdateSliderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _sliderRepository.GetByIdAsync(request.SliderId);
                if (entity != null)
                {
                    entity.SliderContent = request.SliderContent;
                    if (request.AddedSliderImage != null)
                    {
                        var url = await _fileUploader.UploadFile(request.AddedSliderImage);
                        _fileUploader.DeleteFile(entity.SliderImageUrl!);
                        entity.SliderImageUrl = url;
                    }
                    var validator = new SliderValidator();
                    var validation = validator.Validate(entity);
                    if (validation.IsValid)
                    {
                        var created = await _sliderRepository.UpdateAsync(entity);
                        if (created != null)
                        {
                            return new IDTO<object?>(200, null, ["Güncelleme işleme işlemi başarılı."]);
                        }
                        else
                        {
                            return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
                        }
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, validation.Errors.Select(x => x.ErrorMessage).ToList());
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
