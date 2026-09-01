using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFStockMovementRepository(AppDbContext context)
        : GenericRepository<StockMovement>(context), IStockMovementDAL
    {
        public async Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId, int take = 50)
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
