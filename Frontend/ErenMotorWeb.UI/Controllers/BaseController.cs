using ErenMotorWeb.UI.Core.Dtos.GetResponseDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult HandleErrorResponse<T>(GetApiResponseDto<T> response, string viewName, object? model = null)
        {
            if (response.Errors is null)
            {
                if (response.StatusCode >= 500 && response.StatusCode < 600)
                    return RedirectToAction("Error500", "ErrorPage");

                if (response.StatusCode >= 400 && response.StatusCode < 500)
                    return RedirectToAction("Error404", "ErrorPage");

                return RedirectToAction("GeneralError", "ErrorPage");
            }
            foreach (var error in response.Errors)
            {
                foreach (var message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }
            return View(viewName, model);
        }
    }
}
