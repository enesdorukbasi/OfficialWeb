using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.ThemeDtoProfiles
{
    public class ViewerPageMediaProfile : Profile
    {
        public ViewerPageMediaProfile()
        {
            CreateMap<ViewerPageMedia, ViewerPageMediaListDTO>().ReverseMap();
        }
    }
}
