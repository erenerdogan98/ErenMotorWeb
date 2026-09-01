using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IProductDAL : IGenericDAL<Product>
    {
        Task<IEnumerable<Product>> GetAllWithDetailsAsync();

        Task<Product?> GetByIdWithDetailsAsync(int id);
    }
}
