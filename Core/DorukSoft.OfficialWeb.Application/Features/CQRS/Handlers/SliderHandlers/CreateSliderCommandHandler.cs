using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SliderHandlers
{
    public class CreateSliderCommandHandler : IRequestHandler<CreateSliderCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly FileUploader _fileUploader;

        public CreateSliderCommandHandler(IRepository<Slider> sliderRepository, FileUploader fileUploader)
        {
            _sliderRepository = sliderRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(CreateSliderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.SliderImageUrl != null)
                {
                    var url = await _fileUploader.UploadFile(request.SliderImageUrl);
                    var entity = new Slider
                    {
                        SliderImageUrl = url,
                        SliderContent = request.SliderContent,
                    };
                    var validator = new SliderValidator();
                    var validation = validator.Validate(entity);
                    if (validation.IsValid)
                    {
                        var created = await _sliderRepository.CreateAsync(entity);
                        if(created != null)
                        {
                            return new IDTO<object?>(200, null, ["Slider yüklendi."]);
                        }
                        else
                        {
                            return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
                        }
                    }
                    else
                    {
                        _fileUploader.DeleteFile(url);
                        return new IDTO<object?>(300, null, validation.Errors.Select(x=>x.ErrorMessage).ToList());
                    }
                }
                return new IDTO<object?>(300, null, ["Görsel seçmek zorunludur."]);
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
