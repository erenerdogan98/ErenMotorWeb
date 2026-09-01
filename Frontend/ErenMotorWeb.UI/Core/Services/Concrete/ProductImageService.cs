using ErenMotorWeb.UI.Core.Dtos.ProductImageDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class ProductImageService(HttpClientHelper httpClient) : GenericService<CreateProductImageDto, UpdateProductImageDto, ResultProductImageDto, int>(httpClient), IProductImageService
    {
    }
}
