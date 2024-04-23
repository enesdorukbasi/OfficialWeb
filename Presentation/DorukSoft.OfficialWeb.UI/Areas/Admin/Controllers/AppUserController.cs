using DorukSoft.OfficialWeb.Application.Features.CQRS.Queries.AppUserQueries;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using DorukSoft.OfficialWeb.Application.Features.CQRS.Commands.AppUserCommands;
using DorukSoft.OfficialWeb.Application.DTOs;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AppUser")]
    public class AppUserController : Controller
    {
        private readonly IMediator _mediator;

        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var allUsers = await _mediator.Send(new GetAllUsersQueryRequest());
            return View(allUsers.data);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View(new RegisterAppUserCommandRequest());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(RegisterAppUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.status == 200)
            {
                return RedirectToAction("List", "AppUser", new { area = "Admin" });
            }
            foreach (var error in response.messages ?? [])
            {
                ModelState.AddModelError("", error);
            }
            return View(request);
        }

        [HttpGet("{id}")]
        [Route("Update")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _mediator.Send(new GetByIdAppUserQueryRequest { UserId = id });
            if (response.status == 200)
            {
                return View(response.data);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(AppUserListDTO dto)
        {
            var response = await _mediator.Send(new UpdateAppUserCommandRequest { AppUserId = dto.AppUserId, Email = dto.Email, Name = dto.Name, Surname = dto.Surname, Password = dto.Password });
            if (response.status == 200)
            {
                return RedirectToAction("List", "AppUser", new { area = "Admin" });
            }
            foreach (var error in response.messages ?? [])
            {
                ModelState.AddModelError("", error);
            }
            return View(response.data);
        }

        [HttpDelete("{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteAppUserCommandRequest { UserId = id });
            return RedirectToAction("List", "AppUser", new { area = "Admin" });
        }


        [HttpPut("{id}")]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var response = await _mediator.Send(new ChangeStatusAppUserCommandRequest { UserId = id });
            return RedirectToAction("List", "AppUser", new { area = "Admin" });
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View(new AppUserLoginDTO());
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(AppUserLoginDTO dto)
        {
            var response = await _mediator.Send(new LoginAppUserQueryRequest { Email = dto.Email ?? "", Password = dto.Password ?? "" });
            if (response.status == 200)
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(response.data?.Token);
                var claims = token.Claims.ToList();
                claims.Add(new Claim(ClaimTypes.Role, response.data!.AppRole!.Definition ?? ""));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, response.data?.AppUserId.ToString() ?? ""));
                claims.Add(new Claim("token", response.data?.Token ?? ""));
                claims.Add(new Claim("fullName", response.data?.FullName ?? ""));
                claims.Add(new Claim("email", response.data?.Email ?? ""));
                claims.Add(new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(response.data)));

                // Oturumu açma işlemi
                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties
                {
                    ExpiresUtc = response.data?.TokenExpireDateTime,
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                return View(dto);
            }
        }

        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "AppUser", new { area = "Admin" });
        }
    }
}