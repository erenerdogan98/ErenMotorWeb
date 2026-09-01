using ErenMotorWeb.UI.Core.Dtos.ProductImageDto;

namespace ErenMotorWeb.UI.Core.Services.Abstract
{
    public interface IProductImageService : IGenericService<CreateProductImageDto, UpdateProductImageDto, ResultProductImageDto, int>
    {
    }
}
