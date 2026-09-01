using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.CategoryDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;
using Serilog;

namespace ErenMotorWeb.BLL.Concrete
{
    public class CategoryManager(ICategoryDAL categoryDAL, IMapper mapper) : GenericManager<Category, CreateCategoryDto, UpdateCategoryDto, ResultCategoryDto>(categoryDAL, mapper), ICategoryService
    {
        public async Task<ApiResponseDto<IEnumerable<ResultCategoryDto>>> TGetAllWithParentAsync()
        {
            try
            {
                var categories = await categoryDAL.GetAllWithParentAsync();

                if (categories != null)
                {
                    var categoriesDto = mapper.Map<IEnumerable<ResultCategoryDto>>(categories);

                    return new ApiResponseDto<IEnumerable<ResultCategoryDto>>(
                        categoriesDto,
                        true,
                        200,
                        "Category values fetched successfully with parent information.");
                }

                return new ApiResponseDto<IEnumerable<ResultCategoryDto>>(
                    null,
                    false,
                    404,
                    "Category values not found.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching categories with parent information.");

                return new ApiResponseDto<IEnumerable<ResultCategoryDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<ResultCategoryDto>> TGetByIdWithParentAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ApiResponseDto<ResultCategoryDto>(
                        default,
                        false,
                        422,
                        "Invalid Id");
                }

                var category = await categoryDAL.GetByIdWithParentAsync(id);

                if (category != null)
                {
                    var categoryDto = mapper.Map<ResultCategoryDto>(category);

                    return new ApiResponseDto<ResultCategoryDto>(
                        categoryDto,
                        true,
                        200,
                        "Category fetched successfully with parent information.");
                }

                return new ApiResponseDto<ResultCategoryDto>(
                    default,
                    false,
                    404,
                    "Category not found.");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "An error occurred while fetching category with parent information by Id: {Id}",
                    id);

                return new ApiResponseDto<ResultCategoryDto>(
                    default,
                    false,
                    500,
                    ex.Message);
            }
        }
    }
}
