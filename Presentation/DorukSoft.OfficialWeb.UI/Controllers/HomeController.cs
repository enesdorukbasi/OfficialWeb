using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ContactCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BlogQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.MainPage;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.PageQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries;
using DorukSoft.OfficialWeb.Application.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DorukSoft.OfficialWeb.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly RabbitMQService _rabbitMQService;

        public HomeController(IMediator mediator, RabbitMQService rabbitMQService)
        {
            _mediator = mediator;
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet]
        public async Task<IActionResult> MainPage()
        {
            var response = await _mediator.Send(new GetAllDataForMainPageQueryRequest());
            return View(response.data);
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var response = await _mediator.Send(new GetAllProductQueryRequest());
            if (response.status == 200)
            {
                return PartialView("_Products", response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            if (response.status == 200)
            {
                return PartialView("_Categories", response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Blogs()
        {
            var response = await _mediator.Send(new GetAllBlogQueryRequest());
            if (response.status == 200)
            {
                return PartialView("_Blogs", response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async  Task<IActionResult> GetBlogDetails([FromQuery(Name = "id")] int id)
        {
            var response = await _mediator.Send(new GetBlogByIdQueryRequest { BlogId = id });
            if(response.status == 200)
            {
                return PartialView("_GetBlogDetails", response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var response = await _mediator.Send(new GetAboutPageQueryRequest());
            if (response.status == 200)
            {
                return PartialView("_About", response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGenericPageAsync([FromQuery(Name = "id")] int id)
        {
            var response = await _mediator.Send(new GetByIdGenericPageQueryRequest { PageId = id });
            return PartialView("_GenericPage", response.data);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return PartialView("_Contact", new CreateContactDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Contact(CreateContactDTO dto)
        {
            var response = await _mediator.Send(new CreateContactCommandRequest
            {
                Content = dto.Content,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
            });
            if (response.status == 200)
            {
                TempData["Alerts"] = response.messages;
                return RedirectToAction("MainPage", "Home");
            }
            if (response.status == 300)
            {
                if (response.messages != null)
                {
                    foreach (var message in response.messages)
                    {
                        ModelState.AddModelError("", message);
                    }
                }
            }
            TempData["Alerts"] = response.messages;
            return RedirectToAction("MainPage", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategories([FromQuery(Name = "id")] int id)
        {
            var responseCategory = await _mediator.Send(new GetByIdCategoryWithSubCategoriesQueryRequest { CategoryId = id });
            if (responseCategory.status == 200)
            {
                if (responseCategory.data!.SubCategories != null && responseCategory.data.SubCategories.Count > 0)
                {
                    return PartialView("_Categories", responseCategory.data!.SubCategories);
                }
                else
                {
                    var responseProduct = await _mediator.Send(new GetAllProductByCategoryIdQueryRequest { CategoryId = id });
                    if (responseProduct.status == 200)
                    {
                        return PartialView("_Products", responseProduct.data);
                    }
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails([FromQuery(Name = "id")] int id)
        {
            var product = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            if (product != null)
            {
                return PartialView("_ProductDetails", product.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ContactForProduct([FromBody] CreateContactForProductDTO dto)
        {
            var response = await _mediator.Send(new CreateContactForProductCommandRequest
            {
                Content = dto.Content,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                ProductId = dto.ProductId,
            });
            if (response.status == 200)
            {
                return Json(new { status = 200, messages = new List<string> { "Mesajınız iletildi." } });
            }
            else if (response.status == 300)
            {
                return Json(new { status = 300, messages = response.messages });
            }
            else
            {
                return Json(new { status = 500, messages = new List<string> { "Mesajınız iletilemedi." } });
            }
        }
    }
}
