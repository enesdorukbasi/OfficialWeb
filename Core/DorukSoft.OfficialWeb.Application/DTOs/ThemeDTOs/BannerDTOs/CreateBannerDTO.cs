using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CreateBannerDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsShowMainPage { get; set; }
        public bool IsShowAboutPage { get; set; }
    }
}
