using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.BlogHandlers
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly FileUploader _fileUploader;

        public DeleteBlogCommandHandler(IRepository<Blog> blogRepository, FileUploader fileUploader)
        {
            _blogRepository = blogRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(DeleteBlogCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _blogRepository.GetByIdAsync(request.BlogId);
                if (entity != null)
                {
                    var isDeleted = await _blogRepository.DeleteAsync(entity);
                    if (isDeleted)
                    {
                        _fileUploader.DeleteFile(entity.ImageUrl!);
                        return new IDTO<object?>(200, null, ["Bir sorun oluştu."]);
                    }
                    else
                    {
                        return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
                    }
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Blog bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
