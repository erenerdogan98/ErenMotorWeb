using ErenMotorWeb.UI.Core.Services.Abstract;
using ErenMotorWeb.UI.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.ViewComponents
{
    public class SupplierSelectViewComponent(ISupplierService supplierService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? selectedSupplierId = null)
        {
            var response = await supplierService.GetAllAsync();
            var values = response.Data;
            if (!response.Success)
                return HandleError(response.StatusCode);
            ViewBag.SelectedSupplierId = selectedSupplierId;
            return View(values);
        }
    }
}
