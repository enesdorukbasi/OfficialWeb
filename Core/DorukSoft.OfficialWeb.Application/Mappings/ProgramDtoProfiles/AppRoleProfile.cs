using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.Entities;

namespace DorukSoft.OfficialWeb.Application.Mappings
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRole, AppRoleListDTO>().ReverseMap();
        }
    }
}
