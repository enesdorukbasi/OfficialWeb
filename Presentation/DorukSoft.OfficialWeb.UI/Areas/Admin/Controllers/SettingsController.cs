using DorukSoft.OfficialWeb.Application.DTOs;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.BannerCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.CompanyInformationCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SliderCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.SocialMediaCommands;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.BannerQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.CompanyInformationQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.SettingsPageQueries;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.SliderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Settings")]
    public class SettingsController : Controller
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("SettingsPage")]
        public async Task<IActionResult> SettingsPage()
        {
            var response = await _mediator.Send(new GetSettingsPageQueryRequest());
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
        [Route("UpdateCompanyInformation")]
        public async Task<IActionResult> UpdateCompanyInformation()
        {
            var response = await _mediator.Send(new GetCompanyInformationQueryRequest());
            if (response.status == 200)
            {
                return View(response.data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("UpdateCompanyInformation")]
        public async Task<IActionResult> UpdateCompanyInformation(UpdateCompanyInformationDTO dto)
        {
            var response = await _mediator.Send(new UpdateCompanyInformationCommandRequest
            {
                CompanyInformationId = dto.CompanyInformationId,
                CompanyName = dto.CompanyName,
                CompanyTitle = dto.CompanyTitle,
                PhoneNumber = dto.PhoneNumber,
                WhatsappPhoneNumber = dto.WhatsappPhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                AddedImage = dto.AddedImage,
                ImageUrl = dto.ImageUrl,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
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

        [HttpGet]
        [Route("CreateSocialMedia")]
        public IActionResult CreateSocialMedia()
        {
            return View(new CreateSocialMediaDTO());
        }

        [HttpPost]
        [Route("CreateSocialMedia")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDTO dto)
        {
            var response = await _mediator.Send(new CreateSocialMediaCommandRequest
            {
                Url = dto.Url,
                Icon = dto.Icon,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
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

        [HttpPost("{id}")]
        [Route("DeleteSocialMedia")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var response = await _mediator.Send(new DeleteSocialMediaCommandRequest
            {
                SocialMediaId = id,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("CreateSlider")]
        public IActionResult CreateSlider()
        {
            return View(new CreateSliderDTO());
        }

        [HttpPost]
        [Route("CreateSlider")]
        public async Task<IActionResult> CreateSlider(CreateSliderDTO dto)
        {
            var response = await _mediator.Send(new CreateSliderCommandRequest
            {
                SliderContent = dto.SliderContent,
                SliderImageUrl = dto.SliderImageUrl,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
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

        [HttpGet("{id}")]
        [Route("UpdateSlider")]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var response = await _mediator.Send(new GetSliderByIdQueryRequest { SliderId = id });
            if (response.status == 200)
            {
                return View(new UpdateSliderDTO
                {
                    SliderId = response.data!.SliderId,
                    SliderContent = response.data.SliderContent,
                    SliderImageUrl = response.data.SliderImageUrl,
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("UpdateSlider")]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDTO dto)
        {
            var response = await _mediator.Send(new UpdateSliderCommandRequest
            {
                SliderContent = dto.SliderContent,
                SliderImageUrl = dto.SliderImageUrl,
                AddedSliderImage = dto.AddedSliderImage,
                SliderId = dto.SliderId,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
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

        [HttpPost("{id}")]
        [Route("DeleteSlider")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var response = await _mediator.Send(new DeleteSliderCommandRequest { SliderId = id });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
            }
            return NotFound();
        }

        [HttpGet]
        [Route("CreateBanner")]
        public IActionResult CreateBanner()
        {
            return View(new CreateBannerDTO());
        }

        [HttpPost]
        [Route("CreateBanner")]
        public async Task<IActionResult> CreateBanner(CreateBannerDTO dto)
        {
            var response = await _mediator.Send(new CreateBannerCommandRequest
            {
                Title = dto.Title,
                Content = dto.Content,
                IsShowMainPage = dto.IsShowMainPage,
                IsShowAboutPage =  dto.IsShowAboutPage,
                Image = dto.Image,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
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


        [HttpGet("{id}")]
        [Route("UpdateBanner")]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var response = await _mediator.Send(new GetBannerByIdQueryRequest { BannerId = id });
            if (response.status == 200)
            {
                return View(new UpdateBannerDTO
                {
                    BannerId = response.data!.BannerId,
                    Title = response.data.Title,
                    Content = response.data.Content,
                    ImageUrl = response.data.ImageUrl,
                    IsShowMainPage = response.data.IsShowMainPage,
                    IsShowAboutPage = response.data.IsShowAboutPage,
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("UpdateBanner")]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDTO dto)
        {
            var response = await _mediator.Send(new UpdateBannerCommandRequest
            {
                Title = dto.Title,
                Content = dto.Content,
                IsShowMainPage = dto.IsShowMainPage,
                IsShowAboutPage = dto.IsShowAboutPage,
                ImageUrl = dto.ImageUrl,
                AddedImage = dto.AddedImage,
                BannerId = dto.BannerId,
            });
            if (response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
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

        [HttpPost("{id}")]
        [Route("DeleteBanner")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var response = await _mediator.Send(new DeleteBannerCommandRequest { BannerId = id });
            if(response.status == 200)
            {
                return RedirectToAction("SettingsPage", "Settings", new { Area = "Admin" });
            }
            return NotFound();
        }
    }
}
