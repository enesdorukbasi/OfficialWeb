using Microsoft.AspNetCore.Mvc;

namespace DorukSoft.OfficialWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
