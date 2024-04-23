using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands
{
    public class UpdateBlogCommandRequest : IRequest<IDTO<object?>>
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? AddedImage { get; set; }
        public string? ImageUrl { get; set; }
        public string? Keywords { get; set; }
        public bool IsShowMainPage { get; set; }
    }
}
