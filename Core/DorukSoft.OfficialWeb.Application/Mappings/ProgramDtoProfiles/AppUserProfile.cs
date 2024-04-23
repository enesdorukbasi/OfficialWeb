using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.Entities;

namespace DorukSoft.OfficialWeb.Application.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserListDTO>().ReverseMap();
            CreateMap<AppUser, AppUserLoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterAppUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();
        }
    }
}
