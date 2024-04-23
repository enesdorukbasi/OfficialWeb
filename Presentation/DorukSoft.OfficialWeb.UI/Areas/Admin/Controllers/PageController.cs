using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.PageCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries;
using DorukSoft.OfficialWeb.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Page")]
    public class PageController : Controller
    {
        private readonly IMediator _mediator;

        public PageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var pages = await _mediator.Send(new GetAllPageQueryRequest());
            return View(pages.data);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            SelectList pageTypes = new SelectList(Enum.GetValues(typeof(GenericPageType)));
            ViewBag.PageTypes = pageTypes;
            return View(new CreateGenericPageDTO());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateGenericPageDTO dto)
        {
            if (dto != null)
            {
                if (!string.IsNullOrEmpty(dto.ListItemJson))
                {
                    dto.ListItem = JsonSerializer.Deserialize<List<string>>(dto.ListItemJson);
                }
                var response = await _mediator.Send(new CreateGenericPageCommandRequest()
                {
                    PageTitle = dto.PageTitle,
                    PageContent = dto.PageContent,
                    ListItem = dto.ListItem,
                    ViewerPageMedias = dto.ViewerPageMedias,
                    PageType = dto.PageType
                });
                if (response.status == 200)
                {
                    return RedirectToAction("List", "Page", new { area = "Admin" });
                }
                else if (response.status == 300)
                {
                    SelectList pageTypes = new SelectList(Enum.GetValues(typeof(GenericPageType)));
                    ViewBag.PageTypes = pageTypes;
                    if (response.messages!.Count > 0)
                    {
                        foreach (var message in response.messages)
                        {
                            ModelState.AddModelError("", message);
                        }
                    }
                    return View(dto);
                }
                else
                {
                    SelectList pageTypes = new SelectList(Enum.GetValues(typeof(GenericPageType)));
                    ViewBag.PageTypes = pageTypes;
                    return View(dto);
                }
            }
            return View(dto);
        }

        [HttpGet("{id}")]
        [Route("UpdateGenericPage")]
        public async Task<IActionResult> UpdateGenericPage(int id)
        {
            SelectList pageTypes = new SelectList(Enum.GetValues(typeof(GenericPageType)));
            ViewBag.PageTypes = pageTypes;
            var response = await _mediator.Send(new GetByIdGenericPageQueryRequest { PageId = id });
            return View(new UpdateGenericPageDTO
            {
                PageId = response.data!.PageId,
                PageTitle = response.data.PageTitle,
                PageContent = response.data.PageContent,
                PageType = response.data.PageType,
                ListItem = response.data.ListItem,
                AddedListItemJson = JsonSerializer.Serialize(response.data.ListItem),
                ViewerPageMedias = response.data.ViewerPageMedias
            });
        }

        [HttpPost]
        [Route("UpdateGenericPage")]
        public async Task<IActionResult> UpdateGenericPage(UpdateGenericPageDTO dto)
        {
            var response = await _mediator.Send(new UpdateGenericPageCommandRequest
            {
                PageId = dto.PageId,
                PageTitle = dto.PageTitle,
                PageContent = dto.PageContent,
                PageType = dto.PageType,
                ListItem = dto.AddedListItemJson != null ? JsonSerializer.Deserialize<List<string>>(dto.AddedListItemJson) : [],
                ViewerPageMedias = dto.ViewerPageMedias,
                AddedViewerPageMedias = dto.AddedViewerPageMedias,
                DeletedViewerPageMedias = dto.DeletedViewerPageMediasJSON != null ? JsonSerializer.Deserialize<List<string>>(dto.DeletedViewerPageMediasJSON) : [],
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Page", new { area = "Admin" });
            }
            else if (response.status == 300)
            {
                SelectList pageTypes = new SelectList(Enum.GetValues(typeof(GenericPageType)));
                ViewBag.PageTypes = pageTypes;
                if (response.messages!.Count > 0)
                {
                    foreach (var message in response.messages)
                    {
                        ModelState.AddModelError("", message);
                    }
                }
                return View(dto);
            }
            else
            {
                SelectList pageTypes = new SelectList(Enum.GetValues(typeof(GenericPageType)));
                ViewBag.PageTypes = pageTypes;
                return View(dto);
            }
        }

        [HttpGet]
        [Route("UpdateAboutPage")]
        public async Task<IActionResult> UpdateAboutPage()
        {
            var response = await _mediator.Send(new GetAboutPageQueryRequest());
            return View(response.data);
        }

        [HttpPost]
        [Route("UpdateAboutPage")]
        public async Task<IActionResult> UpdateAboutPage(AboutPageListDTO dto)
        {
            var response = await _mediator.Send(new UpdateAboutPageCommandRequest
            {
                AboutId = dto.AboutId,
                Title = dto.Title,
                Content = dto.Content,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Page", new { Area = "Admin" });
            }
            else if (response.status == 300)
            {
                if (response.messages!.Count > 0)
                {
                    foreach (var message in response.messages)
                    {
                        ModelState.AddModelError("", message);
                    }
                }
                return View(dto);
            }
            else
            {
                return View(dto);
            }
        }

        [HttpPost("{id}")]
        [Route("DeleteGenericPage")]
        public async Task<IActionResult> DeleteGenericPage(int id)
        {
            var response = await _mediator.Send(new DeleteGenericPageCommandRequest { PageId = id });
            return RedirectToAction("List", "Page", new { Area = "Admin" });
        }
    }
}
