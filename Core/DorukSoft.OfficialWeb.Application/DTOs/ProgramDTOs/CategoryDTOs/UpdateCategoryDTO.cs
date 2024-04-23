using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class UpdateCategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ChangedImage { get; set; }
        public string? Keywords { get; set; }
        public string? Content { get; set; }
        public bool IsShowMainPage { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsMainCategory { get; set; }
        public List<CategoryListDTO>? Categories { get; set; }
    }
}
