using ErenMotorWeb.UI.Core.Constants;
using ErenMotorWeb.UI.Core.Dtos.SupplierDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class SupplierService(HttpClientHelper httpClient) : GenericService<CreateSupplierDto, UpdateSupplierDto, ResultSupplierDto, int>(httpClient, ApiEndpoints.Supplier.Base), ISupplierService
    {
    }
}
