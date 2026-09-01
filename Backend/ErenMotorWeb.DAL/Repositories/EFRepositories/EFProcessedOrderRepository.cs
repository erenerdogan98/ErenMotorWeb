using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFProcessedOrderRepository(AppDbContext context)
        : GenericRepository<ProcessedOrder>(context), IProcessedOrderDAL
    {
        public async Task<bool> ExistsAsync(int platformConnectionId, string platformOrderId, int productId)
        {
            return await dbSet
                .AsNoTracking()
                .AnyAsync(x =>
                    x.PlatformConnectionId == platformConnectionId &&
                    x.PlatformOrderId == platformOrderId &&
                    x.ProductId == productId);
        }

        public async Task<ProcessedOrder?> GetByOrderAsync(int platformConnectionId, string platformOrderId, int productId)
        {
            return await dbSet
                .FirstOrDefaultAsync(x =>
                    x.PlatformConnectionId == platformConnectionId &&
                    x.PlatformOrderId == platformOrderId &&
                    x.ProductId == productId);
        }
    }
}
