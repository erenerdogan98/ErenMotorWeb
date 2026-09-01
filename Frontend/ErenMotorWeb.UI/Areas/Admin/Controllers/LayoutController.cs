using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LayoutController : Controller
    {       
        public IActionResult AdminLayout()
        {
            return View();
        }
    }
}
