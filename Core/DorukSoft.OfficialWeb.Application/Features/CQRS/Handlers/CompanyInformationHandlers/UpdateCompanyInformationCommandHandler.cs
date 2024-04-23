using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CompanyInformationCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CompanyInformationHandlers
{
    public class UpdateCompanyInformationCommandHandler : IRequestHandler<UpdateCompanyInformationCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<CompanyInformation> _companyInformationRepository;
        private readonly FileUploader _fileUploader;

        public UpdateCompanyInformationCommandHandler(IRepository<CompanyInformation> companyInformationRepository, FileUploader fileUploader)
        {
            _companyInformationRepository = companyInformationRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(UpdateCompanyInformationCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _companyInformationRepository.GetByIdAsync(request.CompanyInformationId);
                if (entity != null)
                {
                    entity.CompanyName = request.CompanyName;
                    entity.CompanyTitle = request.CompanyTitle;
                    entity.WhatsappPhoneNumber = request.WhatsappPhoneNumber;
                    entity.PhoneNumber = request.PhoneNumber;
                    entity.Email = request.Email;
                    entity.Address = request.Address;

                    if (request.AddedImage != null)
                    {
                        var url = await _fileUploader.UploadFile(request.AddedImage);
                        if (entity.ImageUrl != null && entity.ImageUrl != "")
                        {
                            _fileUploader.DeleteFile(entity.ImageUrl);
                        }
                        entity.ImageUrl = url;
                    }

                    var validator = new CompanyInformationValidator();
                    var validate = validator.Validate(entity);
                    if (validate.IsValid)
                    {
                        var updated = await _companyInformationRepository.UpdateAsync(entity);
                        if (updated != null)
                        {
                            return new IDTO<object?>(200, null, ["Güncelleme başarılı."]);
                        }
                    }
                    return new IDTO<object?>(300, null, validate.Errors.Select(x => x.ErrorMessage).ToList());
                }
                return new IDTO<object?>(404, null, ["Data bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
