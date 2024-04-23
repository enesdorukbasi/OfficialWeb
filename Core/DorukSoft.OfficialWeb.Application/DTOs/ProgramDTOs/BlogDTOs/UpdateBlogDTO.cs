using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class UpdateBlogDTO
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? AddedImage { get; set; }
        public string? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public bool IsShowMainPage { get; set; }
    }
}
