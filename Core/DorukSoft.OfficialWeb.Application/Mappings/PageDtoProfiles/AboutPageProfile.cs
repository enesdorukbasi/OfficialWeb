using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.PageEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.PageDtoProfiles
{
    public class AboutPageProfile : Profile
    {
        public AboutPageProfile()
        {
            CreateMap<AboutPage, AboutPageListDTO>().ReverseMap();
        }
    }
}
