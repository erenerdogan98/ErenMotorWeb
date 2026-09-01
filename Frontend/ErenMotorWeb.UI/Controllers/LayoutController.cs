using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult UILayout()
        {
            return View();
        }
    }
}
