using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class CreateGenericPageCommandHandler : IRequestHandler<CreateGenericPageCommandRequest, IDTO<GenericPageListDTO?>>
    {
        private readonly IRepository<GenericPage> _genericPageRepository;
        private readonly IRepository<ViewerPageMedia> _viewerPageMediaRepository;
        private readonly FileUploader _fileUploader;
        private readonly IMapper _mapper;

        public CreateGenericPageCommandHandler(IRepository<GenericPage> genericPageRepository, IMapper mapper, IRepository<ViewerPageMedia> viewerPageMediaRepository, FileUploader fileUploader)
        {
            _genericPageRepository = genericPageRepository;
            _mapper = mapper;
            _viewerPageMediaRepository = viewerPageMediaRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<GenericPageListDTO?>> Handle(CreateGenericPageCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createdPage = await _genericPageRepository.CreateAsync(new GenericPage()
                {
                    PageTitle = request.PageTitle,
                    PageContent = request.PageContent,
                    PageType = request.PageType,
                    ListItem = request.ListItem,
                });
                if (request.ViewerPageMedias != null && createdPage != null)
                {
                    foreach (var viewerPageMedias in request.ViewerPageMedias)
                    {
                        var url = await _fileUploader.UploadFile(viewerPageMedias);
                        var extension = Path.GetExtension(url);
                        var uploadedMedia = await _viewerPageMediaRepository.CreateAsync(new ViewerPageMedia()
                        {
                            MediaExtension = extension,
                            MediaUrl = url,
                            Title = viewerPageMedias.FileName,
                            ViewerPageId = createdPage.PageId
                        });
                    }
                    return new IDTO<GenericPageListDTO?>(200, null, ["Sayfa oluşturuldu."]);
                }
                else
                {
                    return new IDTO<GenericPageListDTO?>(200, null, ["Sayfa oluşturuldu."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<GenericPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
