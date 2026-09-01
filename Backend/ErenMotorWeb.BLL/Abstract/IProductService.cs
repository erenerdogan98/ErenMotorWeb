using ErenMotorWeb.Dto.ProductDto;

namespace ErenMotorWeb.BLL.Abstract
{
    public interface IProductService : IGenericService<CreateProductDto, UpdateProductDto, ResultProductDto>
    {
    }
}
