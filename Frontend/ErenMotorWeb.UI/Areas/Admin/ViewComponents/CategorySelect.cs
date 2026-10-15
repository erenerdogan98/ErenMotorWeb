using ErenMotorWeb.UI.Core.Services.Abstract;
using ErenMotorWeb.UI.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.ViewComponents
{
    public class CategorySelect(ICategoryService categoryService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? selectedCategoryId = null)
        {
            var response = await categoryService.TGetAllWithParentAsync();
            var values = response.Data;
            if (!response.Success)
                return HandleError(response.StatusCode);

            ViewBag.SelectedCategoryId = selectedCategoryId;
            return View(values);
        }
    }
}
