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
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, IDTO<CategoryListDTO?>>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly FileUploader _fileUploader;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IMapper mapper, FileUploader fileUploader)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }

        public async Task<IDTO<CategoryListDTO?>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category
                {
                    Name = request.Name,
                    Content = request.Content,
                    Keywords = request.Keywords,
                    IsShowMainPage = request.IsShowMainPage,
                    ParentCategoryId = request.ParentCategoryId,
                };
                var validator = new CategoryValidator();
                var result = validator.Validate(category);
                if(result.IsValid)
                {
                    string imageUrl = await _fileUploader.UploadFile(request.Image!);
                    category.ImageUrl = imageUrl;
                    var created = await _categoryRepository.CreateAsync(category);
                    return new IDTO<CategoryListDTO?>(200, null, ["Kategori oluşturuldu."]);
                }
                return new IDTO<CategoryListDTO?>(300, null, result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception)
            {
                return new IDTO<CategoryListDTO?>(500, null, ["Bir sorun oluştu."]);
            }
        }
    }
}
