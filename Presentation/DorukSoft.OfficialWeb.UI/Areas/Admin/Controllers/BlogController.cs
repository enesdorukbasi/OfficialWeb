using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BlogCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin"), Route("Admin/Blog")]
    public class BlogController : Controller
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("List")]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new GetAllBlogQueryRequest());
            if (response != null)
            {
                return View(response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet, Route("Create")]
        public IActionResult Create()
        {
            return View(new CreateBlogDTO());
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(CreateBlogDTO dto)
        {
            var response = await _mediator.Send(new CreateBlogCommandRequest
            {
                Title = dto.Title,
                Content = dto.Content,
                Keywords = dto.Keywords,
                IsShowMainPage = dto.IsShowMainPage,
                AddedImage = dto.AddedImage,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Blog", new { Area = "Admin" });
            }
            else if (response.status == 300)
            {
                if (response.messages != null)
                {
                    foreach (var message in response.messages)
                    {
                        ModelState.AddModelError("", message);
                    }
                }
            }
            return View(dto);
        }

        [HttpGet("{id}"), Route("Update")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _mediator.Send(new GetBlogByIdQueryRequest { BlogId = id });
            if (response.status == 200)
            {
                return View(new UpdateBlogDTO
                {
                    BlogId = response.data!.BlogId,
                    Title = response.data.Title,
                    Content = response.data.Content,
                    ImageUrl = response.data.ImageUrl,
                    IsShowMainPage = response.data.IsShowMainPage,
                    Keywords = response.data.Keywords,
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(UpdateBlogDTO dto)
        {
            var response = await _mediator.Send(new UpdateBlogCommandRequest
            {
                BlogId = dto.BlogId,
                AddedImage = dto.AddedImage,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
                IsShowMainPage = dto.IsShowMainPage,
                Keywords = dto.Keywords,
                Title = dto.Title,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Blog", new { Area = "Admin" });
            }
            else if (response.status == 300)
            {
                if (response.messages != null)
                {
                    foreach (var message in response.messages)
                    {
                        ModelState.AddModelError("", message);
                    }
                }
            }
            return View(dto);
        }

        [HttpGet("{id}"), Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteBlogCommandRequest { BlogId = id });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Blog", new { Area = "Admin" });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
