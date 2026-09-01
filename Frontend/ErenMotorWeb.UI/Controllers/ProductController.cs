using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
