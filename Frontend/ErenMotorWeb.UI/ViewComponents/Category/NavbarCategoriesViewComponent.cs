using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.ViewComponents.Category
{
    public class NavbarCategoriesViewComponent(ICategoryService categoryService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await categoryService.GetAllAsync("categories");
            var success = response.Success;
            var data = response.Data.Where(x=> x.IsActive && x.ParentCategoryId ==null).ToList();
            var statusCode = response.StatusCode;
            if (!success)
                return HandleError(statusCode);
            return View(data);
        }
    }
}
