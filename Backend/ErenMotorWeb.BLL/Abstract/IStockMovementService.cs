using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.StockMovementDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IStockMovementService
    {
        Task<ApiResponseDto<IEnumerable<ResultStockMovementDto>>>
            TGetByProductIdAsync(int productId, int take = 50);

        Task<ApiResponseDto<ResultStockMovementDto>>
            TCreateMovementAsync(CreateStockMovementDto dto);
    }
}