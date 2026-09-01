using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail(string slug)
        {
            return View();
        }
    }
}
