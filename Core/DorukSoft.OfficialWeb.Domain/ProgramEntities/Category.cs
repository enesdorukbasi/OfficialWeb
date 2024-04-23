using DorukSoft.OfficialWeb.Domain.PageEntities;

namespace DorukSoft.OfficialWeb.Domain.ProgramEntities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public string? Content { get; set; }
        public bool IsShowMainPage { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category>? SubCategories { get; set; }
        public List<Product>? Products { get; set; }
    }
}
