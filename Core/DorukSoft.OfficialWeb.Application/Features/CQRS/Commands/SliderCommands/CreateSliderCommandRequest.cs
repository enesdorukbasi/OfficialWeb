using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands
{
    public class CreateSliderCommandRequest : IRequest<IDTO<object?>>
    {
        public IFormFile? SliderImageUrl { get; set; }
        public string? SliderContent { get; set; }
    }
}
