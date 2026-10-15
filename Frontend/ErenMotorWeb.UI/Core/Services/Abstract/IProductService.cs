using ErenMotorWeb.UI.Core.Dtos.GetResponseDto;
using ErenMotorWeb.UI.Core.Dtos.ProductDto;

namespace ErenMotorWeb.UI.Core.Services.Abstract
{
    public interface IProductService : IGenericService<CreateProductDto, UpdateProductDto, ResultProductDto, int>
    {
        Task<GetApiResponseDto<IEnumerable<ResultProductDto>>> SearchAsync(string search);
        Task<GetApiResponseDto<PagedResultDto<ResultProductDto>>>GetPagedAsync(int page, int pageSize);
        Task<GetApiResponseDto<string>> RemoveAsync(int id);
    }
}
