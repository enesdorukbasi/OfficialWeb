using DorukSoft.OfficialWeb.Domain.PageEntities;

namespace DorukSoft.OfficialWeb.Domain.ProgramEntities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public bool IsShowMainPage { get; set; }
    }
}
