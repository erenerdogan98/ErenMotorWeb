using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static ErenMotorWeb.UI.Core.Constants.ApiEndpoints;

namespace ErenMotorWeb.UI.Controllers
{
    [Route("{controller}")]
    public class ProductController(IProductService productService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await productService.GetAllAsync();
            var product = response.Data;
            var success = response.Success;
            if (success)
                return View(product);
            return HandleErrorResponse(response, "Product", product);
        }

        [HttpGet("Detail")]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var response = await productService.GetByIdAsync(id);
            var product = response.Data;
            var success = response.Success;
            if (success)
                return View(product);
            return HandleErrorResponse(response, "Product", product);
        }

    }
}
