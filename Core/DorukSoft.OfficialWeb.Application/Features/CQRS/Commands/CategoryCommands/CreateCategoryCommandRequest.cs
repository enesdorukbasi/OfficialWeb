using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CategoryCommands
{
    public class CreateCategoryCommandRequest : IRequest<IDTO<CategoryListDTO?>>
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? Keywords { get; set; }
        public string? Content { get; set; }
        public bool IsShowMainPage { get; set; }
        public int? ParentCategoryId { get; set; }
        public int MainPageId { get; set; }
    }
}
