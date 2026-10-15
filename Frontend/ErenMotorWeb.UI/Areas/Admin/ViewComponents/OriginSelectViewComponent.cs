using ErenMotorWeb.UI.Core.Services.Abstract;
using ErenMotorWeb.UI.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.ViewComponents
{
    public class OriginSelectViewComponent(IOriginService originService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? selectedOriginId = null)
        {
            var response = await originService.GetAllAsync();
            var values = response.Data;
            if (!response.Success)
                return HandleError(response.StatusCode);
            ViewBag.SelectedOriginId = selectedOriginId;
            return View(values);
        }
    }
}
