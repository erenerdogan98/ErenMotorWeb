using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.ViewComponents.Category
{
    public class NavbarCategoriesViewComponent(ICategoryService categoryService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await categoryService.TGetAllWithParentAsync();

            if (!response.Success)
                return HandleError(response.StatusCode);

            var categories = response.Data?
                .Where(x => x.IsActive)
                .ToList() ?? [];

            return View(categories);
        }
    }
}
