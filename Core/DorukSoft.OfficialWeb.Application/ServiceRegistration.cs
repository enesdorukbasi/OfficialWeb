using DorukSoft.OfficialWeb.Application.Mappings;
using DorukSoft.OfficialWeb.Application.Mappings.PageDtoProfiles;
using DorukSoft.OfficialWeb.Application.Mappings.ProgramDtoProfiles;
using DorukSoft.OfficialWeb.Application.Mappings.ThemeDtoProfiles;
using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Application.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace DorukSoft.OfficialWeb.Application
{
    public static class ServiceRegistration
    {
        [Obsolete]
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = JwtTokenDefaults.ValidAudience,
                    ValidIssuer = JwtTokenDefaults.ValidIssuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.Cookie.Name = "OfficialWebCookie";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);
                opt.LoginPath = "/Admin/Auth/Login";
                opt.LogoutPath = "/Admin/Auth/LogOut";
            });
            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<AppUserValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<CategoryValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<AboutPageValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<GenericPageValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<ProductValidator>(); 
                opt.RegisterValidatorsFromAssemblyContaining<CompanyInformationValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<SocialMediaValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<BannerValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<ContactValidator>();
                opt.RegisterValidatorsFromAssemblyContaining<BlogValidator>();
            });
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<AppRoleProfile>();
                opt.AddProfile<AppUserProfile>();
                opt.AddProfile<CategoryProfile>();
                opt.AddProfile<BlogProfile>();
                opt.AddProfile<AboutPageProfile>();
                opt.AddProfile<GenericPageProfile>();
                opt.AddProfile<BannerProfile>();
                opt.AddProfile<SliderProfile>();
                opt.AddProfile<ViewerPageMediaProfile>();
                opt.AddProfile<ProductProfile>();
                opt.AddProfile<CompanyInformationProfile>();
                opt.AddProfile<SocialMediaProfile>();
                opt.AddProfile<ContactProfile>();
                opt.AddProfile<ContactAfterChatMessageProfile>();
            });
        }
    }
}