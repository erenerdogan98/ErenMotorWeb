using ErenMotorWeb.UI.Controllers;
using ErenMotorWeb.UI.Core.Dtos.CategoryDto;
using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}")]
    public class CategoryController(ICategoryService categoryService) : BaseController
    {
        [HttpGet("Categories")]
        public async Task<IActionResult> Categories()
        {
            var response = await categoryService.GetAllAsync();
            var values = response.Data;
            var success = response.Success;
            if(success)
                return View(values);
            return HandleErrorResponse(response, "Categories", values);
        }

        [HttpGet("SubCategories/{parentId}")]
        public async Task<IActionResult> SubCategories(int parentId, string categoryName)
        {
            var response = await categoryService.TGetByIdWithParentAsync(parentId);
            var values = response.Data;
            var success = response.Success;
            ViewBag.ParentCategoryName = categoryName;
            if (success)
                return View(values);
            return HandleErrorResponse(response, "Categories", values);
        }

        [HttpGet("AddCategory")]
        public async Task<IActionResult> AddCategory()
        {
            var response = await categoryService.GetAllAsync();

            if (response.Success)
            {
                ViewBag.Categories = response.Data
                    .Where(x => x.IsActive &&
                                x.ParentCategoryId == null)
                    .OrderBy(x => x.Name)
                    .ToList();
            }

            return View();
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategory)
        {
            var response = await categoryService.CreateAsync(createCategory);
            var success = response.Success;
            if (response.Success)
                return RedirectToAction("Categories");
            return HandleErrorResponse(
               response,
               "AddCategory",
               createCategory);
        }
    }
}
