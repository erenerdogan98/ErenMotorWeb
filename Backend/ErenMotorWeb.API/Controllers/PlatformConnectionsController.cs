using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.PlatformConnectionDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformConnectionsController(IPlatformConnectionService platformConnectionService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreatePlatfromConnection([FromBody] CreatePlatformConnectionDto platformConnectionDto)
        {
            var response =
                await platformConnectionService.TAddAsync(platformConnectionDto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveConnections()
        {
            var response =
                await platformConnectionService.TGetActiveConnectionsAsync();

            return StatusCode(response.StatusCode, response);
        }
    }
}
