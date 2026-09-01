using ErenMotorWeb.Dto.ServiceResponseDto;
using System.Linq.Expressions;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IGenericService<TCreateDto, TUpdateDto, TResultDto> where TCreateDto : class
    {
        Task<ApiResponseDto<string>> TAddAsync(TCreateDto entity);
        Task<ApiResponseDto<string>> TDeleteAsync(int id);
        Task<ApiResponseDto<string>> TUpdateAsync(TUpdateDto updateDto);
        Task<ApiResponseDto<IEnumerable<TResultDto>>> TGetAllAsync();
        Task<ApiResponseDto<TResultDto>> TGetByIdAsync(int id);
        ApiResponseDto<TResultDto> TGetById(int id);
        Task<ApiResponseDto<IEnumerable<TResultDto>>> TGetAllAsync(Expression<Func<TResultDto, bool>> filter);
        Task<ApiResponseDto<TResultDto>> TGeyByFilterAsync(Expression<Func<TResultDto, bool>> filter);
    }
}
