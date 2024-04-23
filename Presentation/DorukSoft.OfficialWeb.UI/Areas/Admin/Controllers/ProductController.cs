using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.ProductCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CategoryQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.ProductQueries;
using DorukSoft.OfficialWeb.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var response = await _mediator.Send(new GetAllProductQueryRequest());
            if (response.status == 200)
            {
                return View(response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            SelectList saleTypes = new SelectList(Enum.GetValues(typeof(ProductSalesType)));
            ViewBag.SaleTypes = saleTypes;
            if (response.status == 200)
            {
                return View(new CreateProductDTO
                {
                    Categories = response.data!,
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {
            var response = await _mediator.Send(new CreateProductCommandRequest
            {
                Title = dto.Title,
                Content = dto.Content,
                Keywords = dto.Keywords,
                Price = dto.Price,
                Tax = dto.Tax,
                Discount = dto.Discount,
                Quantity = dto.Quantity,
                ProductSalesType = dto.ProductSalesType,
                CategoryId = dto.CategoryId,
                UpdatedImages = dto.UpdatedImages,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Product", new { Area = "Admin" });
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
            var responseCategories = await _mediator.Send(new GetAllCategoryQueryRequest());
            SelectList saleTypes = new SelectList(Enum.GetValues(typeof(ProductSalesType)));
            ViewBag.SaleTypes = saleTypes;
            if (response.status == 200)
            {
                return View(dto.Categories = responseCategories.data!);
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
            var responseCategories = await _mediator.Send(new GetAllCategoryQueryRequest());
            var responseProduct = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            SelectList saleTypes = new SelectList(Enum.GetValues(typeof(ProductSalesType)));
            ViewBag.SaleTypes = saleTypes;
            if (responseProduct.status == 200 && responseCategories.status == 200)
            {
                return View(new UpdateProductDTO
                {
                    ProductId = responseProduct.data!.ProductId,
                    Title = responseProduct.data!.Title,
                    ImageUrls = responseProduct.data!.ImageUrls,
                    Content = responseProduct.data!.Content,
                    Keywords = responseProduct.data!.Keywords,
                    Price = responseProduct.data!.Price,
                    Tax = responseProduct.data!.Tax,
                    Discount = responseProduct.data!.Discount,
                    Quantity = responseProduct.data!.Quantity,
                    ProductSalesType = responseProduct.data!.ProductSalesType,
                    CategoryId = responseProduct.data!.CategoryId,
                    Categories = responseCategories.data!,
                    Category = responseProduct.data.Category,
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost()]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateProductDTO dto)
        {
            var response = await _mediator.Send(new UpdateProductCommandRequest
            {
                ProductId = dto.ProductId,
                Title = dto.Title,
                ChangedImages = dto.ChangedImages,
                DeletedImageUrls = dto.DeletedImageUrlsJSON != null ? JsonSerializer.Deserialize<List<string>>(dto.DeletedImageUrlsJSON) : null,
                Content = dto.Content,
                Keywords = dto.Keywords,
                Price = dto.Price,
                Tax = dto.Tax,
                Discount = dto.Discount,
                Quantity = dto.Quantity,
                ProductSalesType = dto.ProductSalesType,
                CategoryId = dto.CategoryId,
            });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Product", new { Area = "Admin" });
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
            var responseCategories = await _mediator.Send(new GetAllCategoryQueryRequest());
            SelectList saleTypes = new SelectList(Enum.GetValues(typeof(ProductSalesType)));
            ViewBag.SaleTypes = saleTypes;
            if (response.status == 200)
            {
                return View(dto.Categories = responseCategories.data!);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequest { Id = id });
            if (response.status == 200)
            {
                return RedirectToAction("List", "Product", new { Area = "Admin" });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
