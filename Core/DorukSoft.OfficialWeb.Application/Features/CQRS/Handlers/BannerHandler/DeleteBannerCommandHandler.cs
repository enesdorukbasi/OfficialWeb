using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BannerCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BannerHandler
{
    public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Banner> _bannerRepository;
        private readonly FileUploader _fileUploader;

        public DeleteBannerCommandHandler(IRepository<Banner> bannerRepository, FileUploader fileUploader)
        {
            _bannerRepository = bannerRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(DeleteBannerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _bannerRepository.GetByIdAsync(request.BannerId);
                if (entity != null)
                {
                    bool isDeleted = await _bannerRepository.DeleteAsync(entity);
                    if (isDeleted)
                    {
                        _fileUploader.DeleteFile(entity.ImageUrl!);
                        return new IDTO<object?>(200, null, ["Silme işlemi başarılı."]);
                    }
                    return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
                }
                return new IDTO<object?>(404, null, ["data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
