using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.OriginDto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginsController(IOriginService originService) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetOrigins()
        {
            var response = await originService.TGetAllAsync();

            return CreateApiResponse(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrigin(
            [FromBody] UpdateOriginDto updateOriginDto)
        {
            var response = await originService.TUpdateAsync(updateOriginDto);

            return CreateApiResponse(response);
        }
        [HttpPost("Seed")]
        public async Task<IActionResult> Seed()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Helper",
                "origins.json");

            if (!System.IO.File.Exists(path))
                return NotFound("origins.json bulunamadı.");

            var json = await System.IO.File.ReadAllTextAsync(path);

            var origins = JsonSerializer.Deserialize<List<CreateOriginDto>>(json);

            if (origins == null || origins.Count == 0)
                return BadRequest("JSON içerisinde kayıt bulunamadı.");

            foreach (var origin in origins)
            {
                await originService.TAddAsync(origin);
            }

            return Ok($"{origins.Count} adet menşe başarıyla eklendi.");
        }
    }
}
