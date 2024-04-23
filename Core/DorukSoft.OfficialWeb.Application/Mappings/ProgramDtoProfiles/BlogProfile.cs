using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.ProgramDtoProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogListDTO>().ReverseMap();
        }
    }
}
