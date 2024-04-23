using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BannerCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BannerHandler
{
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Banner> _bannerRepository;
        private readonly FileUploader _fileUploader;

        public UpdateBannerCommandHandler(IRepository<Banner> bannerRepository, FileUploader fileUploader)
        {
            _bannerRepository = bannerRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(UpdateBannerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _bannerRepository.GetByIdAsync(request.BannerId);
                if (entity != null)
                {
                    entity.Title = request.Title;
                    entity.Content = request.Content;
                    entity.IsShowMainPage = request.IsShowMainPage;
                    entity.IsShowAboutPage = request.IsShowAboutPage;
                    if (request.AddedImage != null)
                    {
                        var url = await _fileUploader.UploadFile(request.AddedImage);
                        if(url != null)
                        {
                            _fileUploader.DeleteFile(entity.ImageUrl!);
                            entity.ImageUrl = url;
                        }
                    }
                    var validator = new BannerValidator();
                    var validation = validator.Validate(entity);
                    if (validation.IsValid)
                    {
                        var updated = await _bannerRepository.UpdateAsync(entity);
                        return new IDTO<object?>(200, null, ["Güncelleme işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, validation.Errors.Select(x=>x.ErrorMessage).ToList());
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
