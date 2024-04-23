namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class AboutPageListDTO
    {
        public int AboutId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public BannerListDTO? Banner { get; set; }
    }
}
