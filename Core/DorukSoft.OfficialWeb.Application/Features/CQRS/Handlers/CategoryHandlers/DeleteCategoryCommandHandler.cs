using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CategoryCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CategoryHandlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, IDTO<object?>>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly FileUploader _fileUploader;

        public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository, FileUploader fileUploader)
        {
            _categoryRepository = categoryRepository;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<object?>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _categoryRepository.GetByIdAsync(request.CategoryId);
                if (entity != null)
                {
                    bool isDeleted = await _categoryRepository.DeleteAsync(entity);
                    if (isDeleted)
                    {
                        if (entity.ImageUrl != null)
                        {
                            _fileUploader.DeleteFile(entity.ImageUrl);
                        }
                        return new IDTO<object?>(200, null, ["Kategori silindi."]);
                    }
                    else
                    {
                        return new IDTO<object?>(300, null, ["Kategori silinemedi."]);
                    }
                }
                else
                {
                    return new IDTO<object?>(404, null, ["Kategori bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<object?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
