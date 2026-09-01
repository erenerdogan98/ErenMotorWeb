using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFSyncLogRepository(AppDbContext context)
        : GenericRepository<SyncLog>(context), ISyncLogDAL
    {
        public async Task<IEnumerable<SyncLog>> GetRecentAsync(int take = 100)
        {
            return await dbSet
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<SyncLog>> GetByProductIdAsync(int productId, int take = 50)
        {
            return await dbSet
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(take)
                .ToListAsync();
        }
    }
}
