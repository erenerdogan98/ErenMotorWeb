using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ProductMappingDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;
using Serilog;

namespace ErenMotorWeb.BLL.Concrete
{
    public class ProductMappingManager(
        IProductMappingDAL productMappingDAL,
        IMapper mapper)
        : GenericManager<
            ProductMapping,
            CreateProductMappingDto,
            UpdateProductMappingDto,
            ResultProductMappingDto>(
                productMappingDAL,
                mapper),
          IProductMappingService
    {
        public async Task<ApiResponseDto<IEnumerable<ResultProductMappingDto>>>
            TGetByProductIdAsync(int productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return new ApiResponseDto<IEnumerable<ResultProductMappingDto>>(
                        null,
                        false,
                        422,
                        "Invalid product id.");
                }

                var mappings =
                    await productMappingDAL.GetByProductIdAsync(productId);

                var values =
                    mapper.Map<IEnumerable<ResultProductMappingDto>>(mappings);

                return new ApiResponseDto<IEnumerable<ResultProductMappingDto>>(
                    values,
                    true,
                    200,
                    "Product mappings fetched successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "An error occurred while fetching product mappings.");

                return new ApiResponseDto<IEnumerable<ResultProductMappingDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<ResultProductMappingDto>>
            TGetByProductAndPlatformAsync(
                int productId,
                int platformConnectionId)
        {
            try
            {
                if (productId <= 0 || platformConnectionId <= 0)
                {
                    return new ApiResponseDto<ResultProductMappingDto>(
                        default,
                        false,
                        422,
                        "Invalid id.");
                }

                var mapping =
                    await productMappingDAL
                        .GetByProductAndPlatformAsync(
                            productId,
                            platformConnectionId);

                if (mapping == null)
                {
                    return new ApiResponseDto<ResultProductMappingDto>(
                        default,
                        false,
                        404,
                        "Product mapping not found.");
                }

                var value =
                    mapper.Map<ResultProductMappingDto>(mapping);

                return new ApiResponseDto<ResultProductMappingDto>(
                    value,
                    true,
                    200,
                    "Product mapping fetched successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "An error occurred while fetching product mapping.");

                return new ApiResponseDto<ResultProductMappingDto>(
                    default,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ResultProductMappingDto>>>
            TGetAllWithDetailsAsync()
        {
            try
            {
                var mappings =
                    await productMappingDAL.GetAllWithDetailsAsync();

                var values =
                    mapper.Map<IEnumerable<ResultProductMappingDto>>(mappings);

                return new ApiResponseDto<IEnumerable<ResultProductMappingDto>>(
                    values,
                    true,
                    200,
                    "Product mappings fetched successfully with details.");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "An error occurred while fetching product mappings with details.");

                return new ApiResponseDto<IEnumerable<ResultProductMappingDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }
    }
}