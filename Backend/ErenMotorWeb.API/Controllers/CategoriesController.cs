using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.CategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategorys()
        {
            var response = await categoryService.TGetAllAsync();

            return CreateApiResponse(response);
        }

        [HttpGet("main-categories")]
        public async Task<IActionResult> GetMainCategories()
        {
            var response = await categoryService.TGetAllAsync(x => x.ParentCategoryId == null);
            return CreateApiResponse(response);
        }
        [HttpGet("with-parent")]
        public async Task<IActionResult> GetCategoriesWithParent()
        {
            var response = await categoryService.TGetAllWithParentAsync();
            return CreateApiResponse(response);
        }

        [HttpGet("{id}/with-parent")]
        public async Task<IActionResult> GetCategoryByIdWithParent(int id)
        {
            var response = await categoryService.TGetByIdWithParentAsync(id);
            return CreateApiResponse(response);
        }

        [HttpGet("{id}/sub-category")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var response = await categoryService.TGetAllAsync(x => x.ParentCategoryId == id);
            return CreateApiResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await categoryService.TGetByIdAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryDto createCategoryDto)
        {
            var response = await categoryService.TAddAsync(createCategoryDto);

            return CreateApiResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await categoryService.TDeleteAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(
            [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var response = await categoryService.TUpdateAsync(updateCategoryDto);

            return CreateApiResponse(response);
        }
    }
}
