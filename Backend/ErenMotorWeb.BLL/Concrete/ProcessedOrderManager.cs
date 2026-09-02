using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ProcessedOrderDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class ProcessedOrderManager(
        IProcessedOrderDAL processedOrderDAL,
        IMapper mapper)
        : IProcessedOrderService
    {
        public async Task<ApiResponseDto<ResultProcessedOrderDto>>
     TCreateOrderAsync(CreateProcessedOrderDto dto)
        {
            try
            {
                var exists = await processedOrderDAL.ExistsAsync(
                    dto.PlatformConnectionId,
                    dto.PlatformOrderId,
                    dto.ProductId);

                if (exists)
                {
                    return new ApiResponseDto<ResultProcessedOrderDto>(
                        null,
                        false,
                        409,
                        "Bu sipariş daha önce işlenmiş.");
                }

                var entity = mapper.Map<ProcessedOrder>(dto);

                await processedOrderDAL.AddAsync(entity);

                var result = mapper.Map<ResultProcessedOrderDto>(entity);

                return new ApiResponseDto<ResultProcessedOrderDto>(
                    result,
                    true,
                    200,
                    "İşlenen sipariş kaydı başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<ResultProcessedOrderDto>(
                    null,
                    false,
                    500,
                    $"İşlenen sipariş kaydı oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponseDto<bool>> TExistsAsync(
            int platformConnectionId,
            string platformOrderId,
            int productId)
        {
            try
            {
                var exists = await processedOrderDAL.ExistsAsync(
                    platformConnectionId,
                    platformOrderId,
                    productId);

                return new ApiResponseDto<bool>(
                    exists,
                    true,
                    200,
                    exists
                        ? "Sipariş daha önce işlenmiş."
                        : "Sipariş daha önce işlenmemiş.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>(
                    false,
                    false,
                    500,
                    $"Sipariş kontrol edilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponseDto<ResultProcessedOrderDto?>> TGetByOrderAsync(
            int platformConnectionId,
            string platformOrderId,
            int productId)
        {
            try
            {
                var value = await processedOrderDAL.GetByOrderAsync(
                    platformConnectionId,
                    platformOrderId,
                    productId);

                if (value == null)
                {
                    return new ApiResponseDto<ResultProcessedOrderDto?>(
                        null,
                        false,
                        404,
                        "İşlenen sipariş kaydı bulunamadı.");
                }

                var result = mapper.Map<ResultProcessedOrderDto>(value);

                return new ApiResponseDto<ResultProcessedOrderDto?>(
                    result,
                    true,
                    200,
                    "İşlenen sipariş kaydı başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<ResultProcessedOrderDto?>(
                    null,
                    false,
                    500,
                    $"İşlenen sipariş getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}