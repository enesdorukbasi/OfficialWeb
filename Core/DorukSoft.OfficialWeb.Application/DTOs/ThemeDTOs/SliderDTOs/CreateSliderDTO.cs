using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CreateSliderDTO
    {
        public IFormFile? SliderImageUrl { get; set; }
        public string? SliderContent { get; set; }
    }
}
