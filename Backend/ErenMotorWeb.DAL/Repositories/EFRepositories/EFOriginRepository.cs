using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFOriginRepository(AppDbContext context) : GenericRepository<Origin>(context), IOriginDAL
    {
    }
}
