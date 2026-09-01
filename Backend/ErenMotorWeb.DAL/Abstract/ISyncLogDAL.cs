using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface ISyncLogDAL : IGenericDAL<SyncLog>
    {
        Task<IEnumerable<SyncLog>> GetRecentAsync(int take = 100);

        Task<IEnumerable<SyncLog>> GetByProductIdAsync(int productId, int take = 50);
    }
}
