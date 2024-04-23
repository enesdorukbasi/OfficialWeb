using AutoMapper;
using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CategoryCommands;
using DorukSoft.OfficialWeb.Application.Interfaces;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Handlers.CategoryHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, IDTO<CategoryListDTO?>>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly FileUploader _fileUploader;

        public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository, IMapper mapper, FileUploader fileUploader)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }
        public async Task<IDTO<CategoryListDTO?>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
                if (category != null)
                {
                    if (request.Image != null)
                    {
                        if (request.ImageUrl != null)
                        {
                            _fileUploader.DeleteFile(category.ImageUrl!);
                        }
                        string imageUrl = await _fileUploader.UploadFile(request.Image);
                        category.ImageUrl = imageUrl;
                    }
                    category.Name = request.Name;
                    category.Keywords = request.Keywords;
                    category.Content = request.Content;
                    category.IsShowMainPage = request.IsShowMainPage;
                    category.ParentCategoryId = request.ParentCategoryId;
                    var validator = new CategoryValidator();
                    var result = validator.Validate(category);
                    if (result.IsValid)
                    {
                        var updated = await _categoryRepository.UpdateAsync(category);
                        return new IDTO<CategoryListDTO?>(200, _mapper.Map<CategoryListDTO>(updated), ["Kategori güncellendi."]);
                    }
                    else
                    {
                        return new IDTO<CategoryListDTO?>(300, null, result.Errors.Select(x => x.ErrorMessage).ToList());
                    }
                }
                else
                {
                    return new IDTO<CategoryListDTO?>(404, null, ["Kategori bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new IDTO<CategoryListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
