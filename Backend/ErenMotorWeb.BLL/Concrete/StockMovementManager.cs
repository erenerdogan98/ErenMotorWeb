using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.StockMovementDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class StockMovementManager(
        IStockMovementDAL stockMovementDAL,
        IMapper mapper)
        : IStockMovementService
    {
        public async Task<ApiResponseDto<ResultStockMovementDto>>
      TCreateMovementAsync(CreateStockMovementDto dto)
        {
            try
            {
                var entity = mapper.Map<StockMovement>(dto);

                await stockMovementDAL.AddAsync(entity);

                var result = mapper.Map<ResultStockMovementDto>(entity);

                return new ApiResponseDto<ResultStockMovementDto>(
                    result,
                    true,
                    200,
                    "Stok hareketi başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<ResultStockMovementDto>(
                    null,
                    false,
                    500,
                    $"Stok hareketi oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ResultStockMovementDto>>> TGetByProductIdAsync(
            int productId,
            int take = 50)
        {
            try
            {
                var values = await stockMovementDAL.GetByProductIdAsync(
                    productId,
                    take);

                var result = mapper.Map<IEnumerable<ResultStockMovementDto>>(values);

                return new ApiResponseDto<IEnumerable<ResultStockMovementDto>>(
                    result,
                    true,
                    200,
                    "Stok hareketleri başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<IEnumerable<ResultStockMovementDto>>(
                    null,
                    false,
                    500,
                    $"Stok hareketleri getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}