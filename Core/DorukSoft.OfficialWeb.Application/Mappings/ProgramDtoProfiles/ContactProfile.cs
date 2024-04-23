using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.ProgramDtoProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactListDTO>().ReverseMap();
            CreateMap<Contact, CreateContactDTO>().ReverseMap();
        }
    }
}
