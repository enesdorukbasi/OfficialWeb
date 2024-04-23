using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BannerCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BannerHandler
{
    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Banner> _bannerRepository;
        private readonly FileUploader _fileUploader;

        public CreateBannerCommandHandler(IRepository<Banner> bannerRepository, FileUploader fileUploader)
        {
            _bannerRepository = bannerRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(CreateBannerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Banner
                {
                    Title = request.Title,
                    Content = request.Content,
                    IsShowMainPage = request.IsShowMainPage,
                    IsShowAboutPage = request.IsShowAboutPage,
                };
                if (request.Image != null)
                {
                    var url = await _fileUploader.UploadFile(request.Image);
                    entity.ImageUrl = url;

                    var validator = new BannerValidator();
                    var validation = validator.Validate(entity);
                    if (validation.IsValid)
                    {
                        var created = await _bannerRepository.CreateAsync(entity);
                        return new IDTO<object?>(200, null, ["Oluşturma işlemi başarılı."]);
                    }
                    else
                    {
                        _fileUploader.DeleteFile(url);
                        return new IDTO<object?>(300, null, validation.Errors.Select(x => x.ErrorMessage).ToList());
                    }
                }
                return new IDTO<object?>(300, null, ["Görsel seçiniz."]);
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
