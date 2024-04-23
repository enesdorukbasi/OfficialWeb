using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SocialMediaHandlers
{
    public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<SocialMedia> _socialMediaRepository;

        public UpdateSocialMediaCommandHandler(IRepository<SocialMedia> socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }
        public async Task<IDTO<object?>> Handle(UpdateSocialMediaCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _socialMediaRepository.GetByIdAsync(request.SocialMediaId);
                if(entity != null)
                {
                    entity.Url = request.Url;
                    entity.Icon = request.Icon;
                    var validator = new SocialMediaValidator();
                    var validate = validator.Validate(entity);
                    if (validate.IsValid)
                    {
                        var update = await _socialMediaRepository.UpdateAsync(entity);
                        if (update != null)
                        {
                            return new IDTO<object?>(200, null, ["Data güncellendi."]);
                        }
                        else
                        {
                            return new IDTO<object?>(500, null, ["Data güncellenemedi."]);
                        }
                    }
                    return new IDTO<object?>(300, null, validate.Errors.Select(x => x.ErrorMessage).ToList());
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
