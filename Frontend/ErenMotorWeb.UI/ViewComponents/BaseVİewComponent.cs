using ErenMotorWeb.UI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.ViewComponents
{
    public abstract class BaseViewComponent : ViewComponent
    {
        protected IViewComponentResult HandleError(int statusCode)
        {
            return ErrorHelperCP.HandleError(statusCode);
        }
    }
}
