using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.SupplierDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController(ISupplierService supplierService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var response = await supplierService.TGetAllAsync();

            return CreateApiResponse(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSupplier(
           [FromBody] CreateSupplierDto supplierDto)
        {
            var response = await supplierService.TAddAsync(supplierDto);

            return CreateApiResponse(response);
        }
    }
}
