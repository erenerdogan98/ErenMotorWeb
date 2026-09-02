using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.SyncLogDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class SyncLogManager(
        ISyncLogDAL syncLogDAL,
        IMapper mapper)
        : ISyncLogService
    {
        public async Task<ApiResponseDto<ResultSyncLogDto>>
    TCreateLogAsync(CreateSyncLogDto dto)
        {
            try
            {
                var entity = mapper.Map<SyncLog>(dto);

                await syncLogDAL.AddAsync(entity);

                var result = mapper.Map<ResultSyncLogDto>(entity);

                return new ApiResponseDto<ResultSyncLogDto>(
                    result,
                    true,
                    200,
                    "Senkronizasyon logu başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<ResultSyncLogDto>(
                    null,
                    false,
                    500,
                    $"Senkronizasyon logu oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ResultSyncLogDto>>> TGetRecentAsync(
            int take = 100)
        {
            try
            {
                var values = await syncLogDAL.GetRecentAsync(take);

                var result = mapper.Map<IEnumerable<ResultSyncLogDto>>(values);

                return new ApiResponseDto<IEnumerable<ResultSyncLogDto>>(
                    result,
                    true,
                    200,
                    "Senkronizasyon logları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<IEnumerable<ResultSyncLogDto>>(
                    null,
                    false,
                    500,
                    $"Senkronizasyon logları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ResultSyncLogDto>>> TGetByProductIdAsync(
            int productId,
            int take = 50)
        {
            try
            {
                var values = await syncLogDAL.GetByProductIdAsync(productId, take);

                var result = mapper.Map<IEnumerable<ResultSyncLogDto>>(values);

                return new ApiResponseDto<IEnumerable<ResultSyncLogDto>>(
                    result,
                    true,
                    200,
                    "Ürünün senkronizasyon logları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<IEnumerable<ResultSyncLogDto>>(
                    null,
                    false,
                    500,
                    $"Ürünün senkronizasyon logları getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}