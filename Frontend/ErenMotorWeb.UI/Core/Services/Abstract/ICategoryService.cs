using ErenMotorWeb.UI.Core.Dtos.CategoryDto;
using ErenMotorWeb.UI.Core.Dtos.GetResponseDto;

namespace ErenMotorWeb.UI.Core.Services.Abstract
{
    public interface ICategoryService : IGenericService<CreateCategoryDto, UpdateCategoryDto, ResultCategoryDto, int>
    {
        Task<GetApiResponseDto<IEnumerable<ResultCategoryDto>>> TGetAllWithParentAsync();
        Task<GetApiResponseDto<IEnumerable<ResultCategoryDto>>> TGetByIdWithParentAsync(int id);
    }
}
