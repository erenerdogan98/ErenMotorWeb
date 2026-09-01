using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IStockMovementDAL : IGenericDAL<StockMovement>
    {
        Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId, int take = 50);
    }
}
