using ErenMotorWeb.Dto.PlatformConnectionDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IPlatformConnectionService : IGenericService<CreatePlatformConnectionDto, UpdatePlatformConnectionDto, ResultPlatformConnectionDto>
    {
        Task<ApiResponseDto<IEnumerable<ResultPlatformConnectionDto>>>TGetActiveConnectionsAsync();

        Task<ApiResponseDto<ResultPlatformConnectionDto>>TGetByPlatformTypeAsync(PlatformTypeDto platformType);
    }
}