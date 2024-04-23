using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BlogHandlers
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly FileUploader _fileUploader;

        public UpdateBlogCommandHandler(IRepository<Blog> blogRepository, FileUploader fileUploader)
        {
            _blogRepository = blogRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(UpdateBlogCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _blogRepository.GetByIdAsync(request.BlogId);
                if (entity != null)
                {
                    var oldImageUrl = entity.ImageUrl;
                    if (request.AddedImage != null)
                    {
                        var addedImageUrl = await _fileUploader.UploadFile(request.AddedImage);
                        entity.ImageUrl = addedImageUrl;
                    }
                    entity.Title = request.Title;
                    entity.Content = request.Content;
                    entity.IsShowMainPage = request.IsShowMainPage;
                    entity.Keywords = request.Keywords;
                    var validator = new BlogValidator();
                    var validation = validator.Validate(entity);
                    if (validation.IsValid)
                    {
                        var updated = await _blogRepository.UpdateAsync(entity);
                        if (updated != null)
                        {
                            if (request.AddedImage != null)
                            {
                                _fileUploader.DeleteFile(oldImageUrl!);
                            }
                            return new IDTO<object?>(200, null, ["Güncelleme işlemi başarılı."]);
                        }
                        if (request.AddedImage != null)
                        {
                            _fileUploader.DeleteFile(entity.ImageUrl!);
                        }
                        return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
                    }
                    else
                    {
                        if (request.AddedImage != null)
                        {
                            _fileUploader.DeleteFile(entity.ImageUrl!);
                        }
                        return new IDTO<object?>(300, null, validation.Errors.Select(x => x.ErrorMessage).ToList());
                    }
                }
                return new IDTO<object?>(404, null, ["Blog bulunamadı."]);
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
