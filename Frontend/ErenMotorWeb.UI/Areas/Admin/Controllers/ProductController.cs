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
        public async Task<IActionResult> Products(int page = 1)
        {
            const int pageSize = 20;

            if (page < 1)
                page = 1;

            var response =
                await productService.GetPagedAsync(
                    page,
                    pageSize);

            if (!response.Success || response.Data == null)
            {
                return HandleErrorResponse(
                    response,
                    "Products",
                    Enumerable.Empty<ResultProductDto>());
            }

            var pagedResult = response.Data;

            ViewBag.CurrentPage =
                pagedResult.Page;

            ViewBag.TotalPages =
                pagedResult.TotalPages;

            ViewBag.PageSize =
                pagedResult.PageSize;

            ViewBag.TotalProducts =
                pagedResult.TotalCount;
            ViewBag.ActiveProducts = pagedResult.ActiveCount;

            ViewBag.OutOfStockProducts = pagedResult.OutOfStockCount;

            return View(pagedResult.Items);
        }

        [HttpGet("SearchProducts")]
        public async Task<IActionResult> SearchProducts(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var allResponse = await productService.GetAllAsync();

                if (allResponse.Success)
                    return PartialView("_ProductTable", allResponse.Data);

                return BadRequest();
            }

            var response = await productService.SearchAsync(search.Trim());

            if (response.Success)
                return PartialView("_ProductTable", response.Data);

            return BadRequest();
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct() 
        {
            return View();
        }

      
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(CreateProductDto createProduct)
        {
            var response = await productService.CreateAsync(createProduct);

            if (response.Success)            
                return RedirectToAction("Products");
            

            return HandleErrorResponse(
                response,
                "AddProduct",
                createProduct);
        }

        [HttpGet("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var response = await productService.GetByIdAsync(id);

            if (response.Success)
                return View(response.Data);

            return HandleErrorResponse(response,"Products",response.Data);
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProduct)
        {
            var response = await productService.UpdateAsync(updateProduct);

            if (response.Success)
                return RedirectToAction("Products");

            return HandleErrorResponse(response,"UpdateProduct",updateProduct);
        }

        [HttpPost("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(int id, int page = 1)
        {
            var response = await productService.RemoveAsync(id);

            if (response.Success)
            {
                TempData["SuccessMessage"] =
                    "Ürün başarıyla kaldırıldı.";
            }
            else
            {
                TempData["ErrorMessage"] =
                    response.Message ?? "Ürün kaldırılırken bir hata oluştu.";
            }

            return RedirectToAction(
                nameof(Products),
                new { page });
        }
    }
}
