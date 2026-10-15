using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.TrendyolDto;
using ErenMotorWeb.Dto.TrendyolDtos;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface ITrendyolProductService
    {
        Task<ApiResponseDto<List<TrendyolProductDto>>> GetOnSaleProductsAsync();
        Task<ApiResponseDto<List<TrendyolProductMatchDto>>> CheckProductMatchesAsync();
    }
}
