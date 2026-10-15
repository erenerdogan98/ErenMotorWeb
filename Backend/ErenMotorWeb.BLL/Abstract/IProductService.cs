using ErenMotorWeb.Dto.ProductDto;
using ErenMotorWeb.Dto.ServiceResponseDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IProductService : IGenericService<CreateProductDto, UpdateProductDto, ResultProductDto>
    {
        Task<ApiResponseDto<ProductImportResultDto>> TImportProductsAsync(IEnumerable<ImportProductRowDto> products);
        Task<ApiResponseDto<PagedResultDto<ResultProductDto>>>TGetPagedAsync(int page, int pageSize);
        Task<ApiResponseDto<string>> TRemoveAsync(int id);
    }
}
