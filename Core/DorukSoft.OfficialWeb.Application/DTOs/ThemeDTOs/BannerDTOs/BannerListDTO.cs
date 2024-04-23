namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class BannerListDTO
    {
        public int BannerId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsShowMainPage { get; set; }
        public bool IsShowAboutPage { get; set; }
    }
}
