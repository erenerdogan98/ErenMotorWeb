using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.ViewComponents.Product
{
    public class ActiveProductsViewComponent(
        IProductService productService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await productService.GetAllAsync();

            if (!response.Success)
                return HandleError(response.StatusCode);

            var products = response.Data?
                .Where(x => x.IsActive && x.Stock >= 1)
                .ToList()
                ?? [];

            return View(products);
        }
    }
}