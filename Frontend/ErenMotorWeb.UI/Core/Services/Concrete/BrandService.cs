using ErenMotorWeb.UI.Core.Constants;
using ErenMotorWeb.UI.Core.Dtos.BrandDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class BrandService(HttpClientHelper httpClient) : GenericService<CreateBrandDto, UpdateBrandDto, ResultBrandDto, int>(httpClient,ApiEndpoints.Brands.Base), IBrandService
    {
    }
}
