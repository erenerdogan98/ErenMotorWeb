using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFBrandRepository(AppDbContext context) : GenericRepository<Brand>(context), IBrandDAL
    {
    }
}
