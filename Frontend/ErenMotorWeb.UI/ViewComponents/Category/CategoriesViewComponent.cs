using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.ViewComponents.Category
{
    public class CategoriesViewComponent(ICategoryService categoryService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await categoryService.TGetAllWithParentAsync();
            var values = response.Data;
            if (!response.Success)
                return HandleError(response.StatusCode);
            return View(values);
        }
    }
}
