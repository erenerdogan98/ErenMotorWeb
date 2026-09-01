using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface ICategoryDAL : IGenericDAL<Category>
    {
        Task<IEnumerable<Category>> GetAllWithParentAsync();

        Task<Category?> GetByIdWithParentAsync(int id);
    }
}
