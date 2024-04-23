using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.SocialMediaHandlers
{
    public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<SocialMedia> _socialMediaRepository;

        public CreateSocialMediaCommandHandler(IRepository<SocialMedia> socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<IDTO<object?>> Handle(CreateSocialMediaCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new SocialMedia
                {
                    Url = request.Url,
                    Icon = request.Icon,
                };
                var validator = new SocialMediaValidator();
                var validate = validator.Validate(entity);
                if (validate.IsValid)
                {
                    var created = await _socialMediaRepository.CreateAsync(entity);
                    if (created != null)
                    {
                        return new IDTO<object?>(200, null, ["Data oluşturuldu."]);
                    }
                    else
                    {
                        return new IDTO<object?>(500, null, ["Data oluşturulamadı."]);
                    }
                }
                return new IDTO<object?>(300, null, validate.Errors.Select(x=>x.ErrorMessage).ToList());
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
