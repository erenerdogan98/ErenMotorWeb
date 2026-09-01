using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.BrandDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService brandService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var response = await brandService.TGetAllAsync();

            return CreateApiResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var response = await brandService.TGetByIdAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(
            [FromBody] CreateBrandDto createBrandDto)
        {
            var response = await brandService.TAddAsync(createBrandDto);

            return CreateApiResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var response = await brandService.TDeleteAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(
            [FromBody] UpdateBrandDto updateBrandDto)
        {
            var response = await brandService.TUpdateAsync(updateBrandDto);

            return CreateApiResponse(response);
        }
    }
}