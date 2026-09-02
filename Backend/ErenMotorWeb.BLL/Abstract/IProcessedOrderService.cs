using ErenMotorWeb.Dto.ProcessedOrderDto;
using ErenMotorWeb.Dto.ServiceResponseDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IProcessedOrderService
    {
        Task<ApiResponseDto<ResultProcessedOrderDto>>
            TCreateOrderAsync(CreateProcessedOrderDto dto);

        Task<ApiResponseDto<bool>>
            TExistsAsync(
                int platformConnectionId,
                string platformOrderId,
                int productId);

        Task<ApiResponseDto<ResultProcessedOrderDto>>
            TGetByOrderAsync(
                int platformConnectionId,
                string platformOrderId,
                int productId);
    }
}