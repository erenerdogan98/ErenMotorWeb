using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.SupplierDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class SupplierManager(ISupplierDAL supplierDAL, IMapper mapper) : GenericManager<Supplier, CreateSupplierDto, UpdateSupplierDto, ResultSupplierDto>(supplierDAL, mapper), ISupplierService
    {
    
    }
}
