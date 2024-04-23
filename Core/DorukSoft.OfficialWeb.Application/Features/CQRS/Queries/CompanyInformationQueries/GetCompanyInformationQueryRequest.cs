using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CompanyInformationQueries
{
    public class GetCompanyInformationQueryRequest : IRequest<IDTO<UpdateCompanyInformationDTO?>>
    {
    }
}
