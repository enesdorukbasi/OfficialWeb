using DorukSoft.OfficialWeb.Application.DTOs;
using MediatR;

namespace DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands
{
    public class DeleteSliderCommandRequest : IRequest<IDTO<object?>>
    {
        public int SliderId { get; set; }
    }
}
