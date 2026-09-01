using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFProductMappingRepository(AppDbContext context)
        : GenericRepository<ProductMapping>(context), IProductMappingDAL
    {
        public async Task<IEnumerable<ProductMapping>> GetByProductIdAsync(int productId)
        {
            return await dbSet
                .AsNoTracking()
                .Include(x => x.PlatformConnection)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductMapping?> GetByProductAndPlatformAsync(int productId, int platformConnectionId)
        {
            return await dbSet
                .FirstOrDefaultAsync(x =>
                    x.ProductId == productId &&
                    x.PlatformConnectionId == platformConnectionId);
        }

        public async Task<IEnumerable<ProductMapping>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.PlatformConnection)
                .ToListAsync();
        }
    }
}
