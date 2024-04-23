namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class PagesListDTO
    {
        public AboutPageListDTO? AboutPage { get; set; }
        public List<GenericPageListDTO>? GenericPages { get; set; }
        public BannerListDTO? Banners { get; set; }
        public string? ImageUrl { get; set; }
    }
}
