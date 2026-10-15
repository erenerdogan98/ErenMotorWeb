using ErenMotorWeb.UI.Core.Constants;
using ErenMotorWeb.UI.Core.Dtos.OriginDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class OriginService(HttpClientHelper httpClient) : GenericService<CreateOriginDto, UpdateOriginDto, ResultOriginDto, int>(httpClient, ApiEndpoints.Origins.Base), IOriginService
    {
    }
}
