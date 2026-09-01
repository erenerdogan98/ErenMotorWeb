using ErenMotorWeb.UI.Controllers;
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
            var response = await categoryService.GetAllAsync("Categories/main-categories");
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
    }
}
