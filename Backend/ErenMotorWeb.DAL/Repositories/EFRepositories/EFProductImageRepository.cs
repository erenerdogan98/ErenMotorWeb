using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFProductImageRepository(AppDbContext context) : GenericRepository<ProductImage>(context), IProductImageDAL
    {
    }
}
