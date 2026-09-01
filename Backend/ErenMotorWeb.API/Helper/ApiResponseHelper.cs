using ErenMotorWeb.Dto.ServiceResponseDto;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ErenMotorWeb.API.Helper
{
    public class ApiResponseHelper
    {
        public static IActionResult CreateApiResponse<T>(
    ApiResponseDto<T> response)
        {
            if (!response.Success)
            {
                Log.Error(
                    "Error: {Message}",
                    response.Message);
            }

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
