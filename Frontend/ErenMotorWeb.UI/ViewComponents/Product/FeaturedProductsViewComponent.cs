using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.ViewComponents.Product
{
    public class FeaturedProductsViewComponent(IProductService productService) : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await productService.GetAllAsync("products");
            var values = response.Data?
                .Where(x => x.IsActive && x.IsFeatured)
                .Take(8)
                .ToList();
            if (!response.Success)
                return HandleError(response.StatusCode);
            return View(values);
        }
    }
}
