using ErenMotorWeb.API.Helper;
using ErenMotorWeb.Dto.ServiceResponseDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult CreateApiResponse<T>(ApiResponseDto<T> response)
        {
            return ApiResponseHelper.CreateApiResponse(response);
        }
    }
}
