using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ContactQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class ContactController : Controller
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new GetAllContactQueryRequest());
            if (response.status == 200)
            {
                return View(response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [Route("ContactDetails")]
        public async Task<IActionResult> ContactDetails(int id)
        {
            var response = await _mediator.Send(new GetContactByIdQueryRequest { ContactId = id, ViewedUserMail = User.Claims.FirstOrDefault(x => x.Type == "email")!.Value });
            if (response.status == 200)
            {
                return View(response.data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
