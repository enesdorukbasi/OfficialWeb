using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class UpdateGenericPageCommandHandler : IRequestHandler<UpdateGenericPageCommandRequest, IDTO<GenericPageListDTO?>>
    {
        private readonly IRepository<GenericPage> _genericPageRepository;
        private readonly IRepository<ViewerPageMedia> _viewerPageMediaRepository;
        private readonly FileUploader _fileUploader;
        private readonly IMapper _mapper;

        public UpdateGenericPageCommandHandler(IRepository<GenericPage> genericPageRepository, IRepository<ViewerPageMedia> viewerPageMediaRepository, FileUploader fileUploader, IMapper mapper)
        {
            _genericPageRepository = genericPageRepository;
            _viewerPageMediaRepository = viewerPageMediaRepository;
            _fileUploader = fileUploader;
            _mapper = mapper;
        }

        public async Task<IDTO<GenericPageListDTO?>> Handle(UpdateGenericPageCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var unchanged = await _genericPageRepository.GetByIdAsync(request.PageId);
                if (unchanged != null)
                {
                    unchanged.PageTitle = request.PageTitle;
                    unchanged.PageContent = request.PageContent;
                    unchanged.ListItem = request.ListItem;
                    var updated = await _genericPageRepository.UpdateAsync(unchanged);
                    if (request.AddedViewerPageMedias != null && request.AddedViewerPageMedias.Count > 0)
                    {
                        foreach (var viewerPageMedias in request.AddedViewerPageMedias)
                        {
                            var url = await _fileUploader.UploadFile(viewerPageMedias);
                            var extension = Path.GetExtension(url);
                            var uploadedMedia = await _viewerPageMediaRepository.CreateAsync(new ViewerPageMedia()
                            {
                                MediaExtension = extension,
                                MediaUrl = url,
                                Title = viewerPageMedias.FileName,
                                ViewerPageId = updated.PageId
                            });
                        }
                    }
                    if (request.DeletedViewerPageMedias != null && request.DeletedViewerPageMedias.Count > 0)
                    {
                        foreach (var viewerPageMedias in request.DeletedViewerPageMedias)
                        {
                            var media = await _viewerPageMediaRepository.GetByFilterAsync(x => x.MediaUrl == viewerPageMedias);
                            if (media != null)
                            {
                                await _viewerPageMediaRepository.DeleteAsync(media);
                                _fileUploader.DeleteFile(viewerPageMedias);
                            }
                        }
                    }
                    return new IDTO<GenericPageListDTO?>(200, null, ["Güncelleme işlemi başarılı."]);
                }
                else
                {
                    return new IDTO<GenericPageListDTO?>(404, null, ["Data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<GenericPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
