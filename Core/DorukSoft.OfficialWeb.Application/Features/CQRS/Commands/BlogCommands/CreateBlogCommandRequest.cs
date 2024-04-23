using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands
{
    public class CreateBlogCommandRequest : IRequest<IDTO<object?>>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? AddedImage { get; set; }
        public string? Keywords { get; set; }
        public bool IsShowMainPage { get; set; }
    }
}
