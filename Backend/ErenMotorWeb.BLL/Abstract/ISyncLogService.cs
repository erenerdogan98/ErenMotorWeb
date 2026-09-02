using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.SyncLogDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface ISyncLogService
    {
        Task<ApiResponseDto<IEnumerable<ResultSyncLogDto>>>
            TGetRecentAsync(int take = 100);

        Task<ApiResponseDto<IEnumerable<ResultSyncLogDto>>>
            TGetByProductIdAsync(int productId, int take = 50);

        Task<ApiResponseDto<ResultSyncLogDto>>
            TCreateLogAsync(CreateSyncLogDto dto);

    }
}