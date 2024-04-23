using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs.ProgramDTOs.ContactAfterChatMessageDTOs;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.ProgramDtoProfiles
{
    public class ContactAfterChatMessageProfile : Profile
    {
        public ContactAfterChatMessageProfile()
        {
            CreateMap<ContactAfterChatMessage, ContactAfterChatMessageListDTO>().ReverseMap();
        }
    }
}
