using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BlogHandlers
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly FileUploader _fileUploader;

        public CreateBlogCommandHandler(IRepository<Blog> blogRepository, FileUploader fileUploader)
        {
            _blogRepository = blogRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(CreateBlogCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Blog
                {
                    Content = request.Content,
                    IsShowMainPage = request.IsShowMainPage,
                    Keywords = request.Keywords,
                    Title = request.Title,
                };
                if (request.AddedImage != null)
                {
                    var url = await _fileUploader.UploadFile(request.AddedImage);
                    entity.ImageUrl = url;
                    var validator = new BlogValidator();
                    var validation = validator.Validate(entity);
                    if (validation.IsValid)
                    {
                        var created = await _blogRepository.CreateAsync(entity);
                        if (created != null)
                        {
                            return new IDTO<object?>(200, null, ["Blog oluşturuldu."]);
                        }
                        else
                        {
                            _fileUploader.DeleteFile(url);
                            return new IDTO<object?>(300, null, ["Bir sorun oluştu."]);
                        }
                    }
                    else
                    {
                        _fileUploader.DeleteFile(url);
                        return new IDTO<object?>(300, null, validation.Errors.Select(x => x.ErrorMessage).ToList());
                    }
                }
                else
                {
                    return new IDTO<object?>(300, null, ["Görsel seçiniz."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
