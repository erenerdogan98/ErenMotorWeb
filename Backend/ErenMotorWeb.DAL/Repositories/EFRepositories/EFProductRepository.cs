using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductDAL
    {
        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Images)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithDetailsAsync(int id)
        {
            return await dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
