using ErenMotorWeb.UI.Core.Dtos.ProductDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class ProductService(HttpClientHelper httpClient) : GenericService<CreateProductDto, UpdateProductDto, ResultProductDto, int>(httpClient), IProductService
    {
    }
}
