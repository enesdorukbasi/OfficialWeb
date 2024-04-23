using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using MediatR;
using System.Linq.Expressions;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.PageHandlers
{
    public class GetByIdGenericPageQueryHandler : IRequestHandler<GetByIdGenericPageQueryRequest, IDTO<GenericPageListDTO?>>
    {
        //private readonly IRepository<GenericPage> _genericPageRepository;
        private readonly IGenericPageRepository _genericPageRepository;
        private readonly IMapper _mapper;
        public GetByIdGenericPageQueryHandler(IGenericPageRepository genericPageRepository, IMapper mapper)
        {
            _genericPageRepository = genericPageRepository;
            _mapper = mapper;
        }

        public async Task<IDTO<GenericPageListDTO?>> Handle(GetByIdGenericPageQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _genericPageRepository.GetGenericPageById(request.PageId);
                if (data != null)
                {
                    return new IDTO<GenericPageListDTO?>(200, _mapper.Map<GenericPageListDTO>(data), ["Data bulundu."]);
                }
                else
                {
                    return new IDTO<GenericPageListDTO?>(404, null, ["Data bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<GenericPageListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
