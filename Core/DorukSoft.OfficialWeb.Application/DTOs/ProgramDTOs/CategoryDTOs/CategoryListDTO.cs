namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CategoryListDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public string? Content { get; set; }
        public bool IsShowMainPage { get; set; }
        public int? ParentCategoryId { get; set; }
        //public int MainPageId { get; set; }
        //public MainPage? MainPage { get; set; }
        public CategoryListDTO? ParentCategory { get; set; }
        public List<CategoryListDTO>? SubCategories { get; set; }
        //public List<Product>? Products { get; set; }
    }
}
