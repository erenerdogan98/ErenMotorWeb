using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ErenMotorWeb.UI.Helper
{
    // For View Components Redirect to Error pages
    public class ErrorHelperCP
    {
        public static IViewComponentResult HandleError(int statusCode)
        {
            if (statusCode >= 500 && statusCode < 600)
                return new ViewViewComponentResult { ViewName = "Error500" };

            if (statusCode >= 400 && statusCode < 500)
                return new ViewViewComponentResult { ViewName = "Error404" };

            return new ViewViewComponentResult { ViewName = "GeneralError" };
        }
    }
}
