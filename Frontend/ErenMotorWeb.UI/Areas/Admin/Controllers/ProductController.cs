using ErenMotorWeb.UI.Controllers;
using ErenMotorWeb.UI.Core.Dtos.ProductDto;
using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}")]
    public class ProductController(IProductService productService) : BaseController
    {
        [HttpGet("Products")]
        public IActionResult Products()
        {
            return View();
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct() 
        {
            return View();
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(CreateProductDto createProduct)
        {
            var response = await productService.CreateAsync(createProduct, "products");
            var success = response.Success;
            if (success)
                return RedirectToAction("Products");
            return (HandleErrorResponse(response, "Products", createProduct));
         
        }
    }
}
