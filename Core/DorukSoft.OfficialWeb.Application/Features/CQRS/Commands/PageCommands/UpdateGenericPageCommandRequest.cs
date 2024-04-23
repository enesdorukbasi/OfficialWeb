using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.Enums;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands
{
    public class UpdateGenericPageCommandRequest : IRequest<IDTO<GenericPageListDTO?>>
    {
        public int PageId { get; set; }
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public GenericPageType PageType { get; set; }
        public List<ViewerPageMediaListDTO>? ViewerPageMedias { get; set; }
        public List<string>? DeletedViewerPageMedias { get; set; }
        public List<IFormFile>? AddedViewerPageMedias { get; set; }
        public List<string>? ListItem { get; set; }
    }
}
