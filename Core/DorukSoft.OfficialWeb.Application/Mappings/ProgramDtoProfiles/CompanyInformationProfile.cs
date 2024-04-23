using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;

namespace DorukSoft.OfficialWeb.Application.Mappings.ProgramDtoProfiles
{
    public class CompanyInformationProfile : Profile
    {
        public CompanyInformationProfile()
        {
            CreateMap<CompanyInformation, CompanyInformationListDTO>().ReverseMap();
            CreateMap<CompanyInformation, CreateCompanyInformationDTO>().ReverseMap();
            CreateMap<CompanyInformation, UpdateCompanyInformationDTO>().ReverseMap();
        }
    }
}
