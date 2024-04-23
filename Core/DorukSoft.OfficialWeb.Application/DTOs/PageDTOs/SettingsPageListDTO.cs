namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class SettingsPageListDTO
    {
        public CompanyInformationListDTO? CompanyInformation { get; set; }
        public List<SocialMediaListDTO>? SocialMedias { get; set; }
        public List<BannerListDTO>? Banners { get; set; }
        public List<SliderListDTO>? Sliders { get; set; }
    }
}
