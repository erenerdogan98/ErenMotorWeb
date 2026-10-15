using ErenMotorWeb.UI.Controllers;
using ErenMotorWeb.UI.Core.Dtos.BrandDto;
using ErenMotorWeb.UI.Core.Services.Abstract;
using ErenMotorWeb.UI.Core.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}")]
    public class BrandController(IBrandService brandService) : BaseController
    {
        [HttpGet("Brands")]
        public async Task<IActionResult> Brands()
        {
            var response = await brandService.GetAllAsync();

            var values = response.Data;
            var success = response.Success;

            if (success)
                return View(values);

            return HandleErrorResponse(response, "Brands", values);
        }

        [HttpGet("AddBrand")]
        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand(CreateBrandDto createBrand)
        {
            var response = await brandService.CreateAsync(createBrand);
            var success = response.Success;
            if (success)
                return RedirectToAction("Brands");
            return (HandleErrorResponse(response, "Brands", createBrand));

        }
    }
}
