using ErenMotorWeb.UI.Core.Dtos.ProductDto;

namespace ErenMotorWeb.UI.Core.Services.Abstract
{
    public interface IProductService : IGenericService<CreateProductDto, UpdateProductDto, ResultProductDto, int>
    {
    }
}
