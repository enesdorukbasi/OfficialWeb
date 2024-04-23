namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class BlogListDTO
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public bool IsShowMainPage { get; set; }
    }
}
