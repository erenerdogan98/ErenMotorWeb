using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IProductDAL : IGenericDAL<Product>
    {
        Task<IEnumerable<Product>> GetAllWithDetailsAsync();
        Task<Product?> GetByIdWithDetailsAsync(int id);
        Task AddRangeAsync(IEnumerable<Product> products);
        Task<(IEnumerable<Product> Items, int TotalCount, int ActiveCount, int OutOfStockCount)>GetPagedWithDetailsAsync(int page, int pageSize);
    }
}
