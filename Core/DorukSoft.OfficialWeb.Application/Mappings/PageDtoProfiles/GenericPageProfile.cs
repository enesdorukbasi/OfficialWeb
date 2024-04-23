using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.PageEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.PageDtoProfiles
{
    public class GenericPageProfile : Profile
    {
        public GenericPageProfile()
        {
            CreateMap<GenericPage, GenericPageListDTO>().ReverseMap();
        }
    }
}
