using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ProductDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;
using Serilog;

namespace ErenMotorWeb.BLL.Concrete
{
    public class ProductManager(IProductDAL productDAL, IMapper mapper)
        : GenericManager<Product, CreateProductDto, UpdateProductDto, ResultProductDto>(productDAL, mapper),
          IProductService
    {
        public override async Task<ApiResponseDto<IEnumerable<ResultProductDto>>> TGetAllAsync()
        {
            try
            {
                var entities = await productDAL.GetAllWithDetailsAsync();
                var dtos = mapper.Map<IEnumerable<ResultProductDto>>(entities);

                return new ApiResponseDto<IEnumerable<ResultProductDto>>(
                    dtos, true, 200, "Product values fetched successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching values Product");
                return new ApiResponseDto<IEnumerable<ResultProductDto>>(null, false, 500, ex.Message);
            }
        }

        public override async Task<ApiResponseDto<ResultProductDto>> TGetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponseDto<ResultProductDto>(default, false, 422, "Invalid Id");

                var entity = await productDAL.GetByIdWithDetailsAsync(id);
                if (entity is null)
                    return new ApiResponseDto<ResultProductDto>(default, false, 404, "Product values not found by Id");

                var dto = mapper.Map<ResultProductDto>(entity);
                return new ApiResponseDto<ResultProductDto>(dto, true, 200, "Product values fecthed successfully by Id");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching data : Product by Id : {Id}", id);
                return new ApiResponseDto<ResultProductDto>(default, false, 500, ex.Message);
            }
        }
    }
}
