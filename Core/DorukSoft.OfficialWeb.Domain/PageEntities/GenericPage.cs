using DorukSoft.OfficialWeb.Domain.Enums;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;

namespace DorukSoft.OfficialWeb.Domain.PageEntities
{
    public class GenericPage
    {
        public int PageId { get; set; }
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public GenericPageType PageType { get; set; }
        public List<ViewerPageMedia>? ViewerPageMedias { get; set; }
        public List<string>? ListItem { get; set; }
    }
}
