using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CategoryCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            return View(response.data);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            return View(new CreateCategoryDTO
            {
                Categories = response.data,
            });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            var response = await _mediator.Send(new CreateCategoryCommandRequest
            {
                Name = dto.Name,
                Content = dto.Content,
                Image = dto.ImageUrl,
                IsShowMainPage = dto.IsShowMainPage,
                Keywords = dto.Keywords,
                ParentCategoryId = dto.IsMainCategory ? null : dto.ParentCategoryId,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Category", new { Area = "Admin" });
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
                return View(dto);
            }
            else
            {
                return View(dto);
            }
        }

        [HttpPost("{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommandRequest { CategoryId = id });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Category", new { Area = "Admin" });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [Route("Update")]
        public async Task<IActionResult> Update(int id)
        {
            var responseCategory = await _mediator.Send(new GetByIdCategoryQueryRequest { CategoryId = id });
            var responseCategories = await _mediator.Send(new GetAllCategoryQueryRequest());
            if (responseCategories.status == 200 && responseCategory.status == 200)
            {
                if (responseCategory.data != null)
                {
                    return View(new UpdateCategoryDTO
                    {
                        CategoryId = responseCategory.data.CategoryId,
                        Name = responseCategory.data.Name,
                        Content = responseCategory.data.Content,
                        Keywords = responseCategory.data.Keywords,
                        ImageUrl = responseCategory.data.ImageUrl,
                        IsShowMainPage = responseCategory.data.IsShowMainPage,
                        IsMainCategory = responseCategory.data.ParentCategoryId != null ? false : true,
                        ParentCategoryId = responseCategory.data.ParentCategoryId,
                        Categories = responseCategories.data,
                    });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCategoryDTO dto)
        {
            var response = await _mediator.Send(new UpdateCategoryCommandRequest
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Content = dto.Content,
                Image = dto.ChangedImage,
                ImageUrl = dto.ImageUrl,
                IsShowMainPage = dto.IsShowMainPage,
                Keywords = dto.Keywords,
                ParentCategoryId = dto.IsMainCategory ? null : dto.ParentCategoryId,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Category", new { Area = "Admin" });
            }
            else if (response.status == 300)
            {
                if (response.messages != null)
                {
                    foreach (var error in response.messages)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(dto);
        }
    }
}
