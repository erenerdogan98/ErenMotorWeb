using ErenMotorWeb.UI.Core.Services.Abstract;
using ErenMotorWeb.UI.Core.Services.Concrete;
using ErenMotorWeb.UI.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.ViewComponents
{
    public class BrandSelectViewComponent(IBrandService brandService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? selectedBrandId = null)
        {
            var response = await brandService.GetAllAsync();
            var values = response.Data;
            if (!response.Success)
                return HandleError(response.StatusCode);

            ViewBag.SelectedBrandId = selectedBrandId;
            return View(values);
        }
    }
}
