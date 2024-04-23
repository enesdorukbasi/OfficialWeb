namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class MainPageListDTO
    {
        public List<GenericPageListDTO>? GenericPages { get; set; }
        public AboutPageListDTO? AboutPage { get; set; }
        public List<SliderListDTO>? Sliders { get; set; }
        public BannerListDTO? Banner { get; set; }
        public CompanyInformationListDTO? CompanyInformation { get; set; }
        public List<CategoryListDTO>? Categories { get; set; }
        public List<BlogListDTO>? Blogs { get; set; }
        public List<SocialMediaListDTO>? SocialMediaLists { get; set; }
    }
}
