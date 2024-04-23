using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands
{
    public class UpdateSliderCommandRequest : IRequest<IDTO<object?>>
    {
        public int SliderId { get; set; }
        public IFormFile? AddedSliderImage { get; set; }
        public string? SliderImageUrl { get; set; }
        public string? SliderContent { get; set; }
    }
}
