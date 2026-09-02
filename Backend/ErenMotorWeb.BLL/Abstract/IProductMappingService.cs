using ErenMotorWeb.Dto.ProductMappingDto;
using ErenMotorWeb.Dto.ServiceResponseDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IProductMappingService
        : IGenericService<
            CreateProductMappingDto,
            UpdateProductMappingDto,
            ResultProductMappingDto>
    {
        Task<ApiResponseDto<IEnumerable<ResultProductMappingDto>>>
            TGetByProductIdAsync(int productId);

        Task<ApiResponseDto<ResultProductMappingDto>>
            TGetByProductAndPlatformAsync(
                int productId,
                int platformConnectionId);

        Task<ApiResponseDto<IEnumerable<ResultProductMappingDto>>>
            TGetAllWithDetailsAsync();
    }
}