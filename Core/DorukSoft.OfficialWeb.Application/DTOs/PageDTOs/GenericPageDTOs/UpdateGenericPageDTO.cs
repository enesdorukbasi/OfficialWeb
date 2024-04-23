using DorukSoft.OfficialWeb.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class UpdateGenericPageDTO
    {
        public int PageId { get; set; }
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public GenericPageType PageType { get; set; }
        public List<ViewerPageMediaListDTO>? ViewerPageMedias { get; set; }
        public List<IFormFile>? AddedViewerPageMedias { get; set; }
        public string? DeletedViewerPageMediasJSON { get; set; }
        public List<string>? ListItem { get; set; }
        public string? AddedListItemJson { get; set; }
    }
}
