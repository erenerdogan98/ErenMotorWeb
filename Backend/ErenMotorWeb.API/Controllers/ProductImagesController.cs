using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.ProductImageDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController(IProductImageService productImageService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProductImages()
        {
            var response = await productImageService.TGetAllAsync();

            return CreateApiResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(int id)
        {
            var response = await productImageService.TGetByIdAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(
            [FromBody] CreateProductImageDto createProductImageDto)
        {            
            var response = await productImageService.TAddAsync(createProductImageDto);

            return CreateApiResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var response = await productImageService.TDeleteAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(
            [FromBody] UpdateProductImageDto updateProductImageDto)
        {
            var response = await productImageService.TUpdateAsync(updateProductImageDto);

            return CreateApiResponse(response);
        }
    }
}
