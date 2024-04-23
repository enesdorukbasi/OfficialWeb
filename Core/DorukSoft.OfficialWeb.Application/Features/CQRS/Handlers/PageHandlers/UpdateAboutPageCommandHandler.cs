using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class UpdateAboutPageCommandHandler : IRequestHandler<UpdateAboutPageCommandRequest, IDTO<AboutPageListDTO?>>
    {
        private readonly IRepository<AboutPage> _aboutPageRepository;
        private readonly IMapper _mapper;

        public UpdateAboutPageCommandHandler(IRepository<AboutPage> aboutPageRepository, IMapper mapper)
        {
            _aboutPageRepository = aboutPageRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<AboutPageListDTO?>> Handle(UpdateAboutPageCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var unchanged = await _aboutPageRepository.GetByIdAsync(request.AboutId);
                if(unchanged != null)
                {
                    unchanged.Title = request.Title;
                    unchanged.Content = request.Content;
                    var updated = await _aboutPageRepository.UpdateAsync(unchanged);
                    if(updated != null)
                    {
                        return new IDTO<AboutPageListDTO?>(200, null, ["Güncelleme işlemi başarılı."]);
                    }
                    else
                    {
                        return new IDTO<AboutPageListDTO?>(300, null, ["Güncelleme işlemi başarısız."]);
                    }
                }
                else
                {
                    return new IDTO<AboutPageListDTO?>(404, null, ["Bu data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<AboutPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
