using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using ErenMotorWeb.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFPlatformConnectionRepository(AppDbContext context)
        : GenericRepository<PlatformConnection>(context), IPlatformConnectionDAL
    {
        // Sync job'ları her çalıştığında sadece aktif bağlantıları çekmek için
        public async Task<IEnumerable<PlatformConnection>> GetActiveConnectionsAsync()
        {
            return await dbSet
                .AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<PlatformConnection?> GetByPlatformTypeAsync(PlatformType platformType)
        {
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PlatformType == platformType && x.IsActive);
        }
    }
}

