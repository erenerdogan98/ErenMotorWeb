using ErenMotorWeb.Dto.CategoryDto;
using ErenMotorWeb.Dto.ServiceResponseDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface ICategoryService : IGenericService<CreateCategoryDto, UpdateCategoryDto, ResultCategoryDto>
    {
        Task<ApiResponseDto<IEnumerable<ResultCategoryDto>>> TGetAllWithParentAsync();

        Task<ApiResponseDto<ResultCategoryDto>> TGetByIdWithParentAsync(int id);
    }
}
