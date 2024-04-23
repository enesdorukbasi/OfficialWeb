using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SocialMediaHandlers
{
    public class DeleteSocialMediaCommandHandler : IRequestHandler<DeleteSocialMediaCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<SocialMedia> _socialMediaRepository;

        public DeleteSocialMediaCommandHandler(IRepository<SocialMedia> socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<IDTO<object?>> Handle(DeleteSocialMediaCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _socialMediaRepository.GetByIdAsync(request.SocialMediaId);
                if(entity != null)
                {
                    bool isDeleted = await _socialMediaRepository.DeleteAsync(entity);
                    if (isDeleted)
                    {
                        return new IDTO<object?>(200, null, ["Silme işlemi başarılı."]);
                    }
                    return new IDTO<object?>(500, null, ["Silme işlemi başarısız."]);
                }
                return new IDTO<object?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
