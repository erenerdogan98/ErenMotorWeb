using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await productService.TGetAllAsync();

            return CreateApiResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await productService.TGetByIdAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductDto createProductDto)
        {
            var response = await productService.TAddAsync(createProductDto);

            return CreateApiResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.TDeleteAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            [FromBody] UpdateProductDto updateProductDto)
        {
            var response = await productService.TUpdateAsync(updateProductDto);

            return CreateApiResponse(response);
        }
    }
}
