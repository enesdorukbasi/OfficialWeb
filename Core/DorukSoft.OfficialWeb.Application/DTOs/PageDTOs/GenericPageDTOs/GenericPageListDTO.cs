using DorukSoft.OfficialWeb.Domain.Enums;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class GenericPageListDTO
    {
        public int PageId { get; set; }
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public GenericPageType PageType { get; set; }
        public List<ViewerPageMediaListDTO>? ViewerPageMedias { get; set; }
        public List<string>? ListItem { get; set; }
    }
}
