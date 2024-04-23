using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class UpdateSliderDTO
    {
        public int SliderId { get; set; }
        public IFormFile? AddedSliderImage { get; set; }
        public string? SliderImageUrl { get; set; }
        public string? SliderContent { get; set; }
    }
}
