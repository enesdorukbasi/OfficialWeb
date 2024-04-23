using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CompanyInformationQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CompanyInformationHandlers
{
    public class GetCompanyInformationQueryHandler : IRequestHandler<GetCompanyInformationQueryRequest, IDTO<UpdateCompanyInformationDTO?>>
    {
        private readonly IRepository<CompanyInformation> _companyInformationRepository;
        private readonly IMapper _mapper;
        public GetCompanyInformationQueryHandler(IRepository<CompanyInformation> companyInformationRepository, IMapper mapper)
        {
            _companyInformationRepository = companyInformationRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<UpdateCompanyInformationDTO?>> Handle(GetCompanyInformationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _companyInformationRepository.GetAllAsync();
                if(entities != null && entities.Count > 0)
                {
                    return new IDTO<UpdateCompanyInformationDTO?>(200, _mapper.Map<UpdateCompanyInformationDTO>(entities.First()), ["Data bulunamadı."]);
                }
                return new IDTO<UpdateCompanyInformationDTO?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<UpdateCompanyInformationDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
