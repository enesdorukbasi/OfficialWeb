using DorukSoft.OfficialWeb.Domain.PageEntities;

namespace DorukSoft.OfficialWeb.Domain.ThemeEntities
{
    public class ViewerPageMedia
    {
        public int MediaId { get; set; }
        public string? Title { get; set; }
        public string? MediaExtension { get; set; }
        public string? MediaUrl { get; set; }
        public int ViewerPageId { get; set; }
        public GenericPage? ViewerPage { get; set; }
    }
}
