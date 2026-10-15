using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFSupplierRepository(AppDbContext context) : GenericRepository<Supplier>(context), ISupplierDAL
    {
    }
}
