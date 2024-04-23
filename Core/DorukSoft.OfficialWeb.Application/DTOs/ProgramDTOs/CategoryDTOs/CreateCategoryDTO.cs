using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CreateCategoryDTO
    {
        public string? Name { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public string? Content { get; set; }
        public bool IsShowMainPage { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsMainCategory { get; set; }
        public List<CategoryListDTO>? Categories { get; set; }
    }
}
