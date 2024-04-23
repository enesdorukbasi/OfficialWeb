using DorukSoft.OfficialWeb.Application;
using DorukSoft.OfficialWeb.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();
services.AddApplicationService();
services.AddPersistenceService(builder.Configuration);

var app = builder.Build();


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllerRoute(
        name: "route",
        pattern: "{controller=Home}/{action=MainPage}/{id?}"
    );
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.Run();
