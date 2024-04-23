namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class ViewerPageMediaListDTO
    {
        public int MediaId { get; set; }
        public string? Title { get; set; }
        public string? MediaExtension { get; set; }
        public string? MediaUrl { get; set; }
        public int ViewerPageId { get; set; }
        public GenericPageListDTO? ViewerPage { get; set; }
    }
}
