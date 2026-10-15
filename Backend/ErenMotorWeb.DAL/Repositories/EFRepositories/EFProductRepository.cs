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
                .Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Supplier)
                .Include(x => x.Origin)
            .Include(x => x.Images.Where(i => i.IsMain))
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithDetailsAsync(int id)
        {
            return await dbSet
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddRangeAsync(IEnumerable<Product> products)
        {
            await dbSet.AddRangeAsync(products);
            await SaveChangesAsync();
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount, int ActiveCount, int OutOfStockCount)>GetPagedWithDetailsAsync(int page, int pageSize)
        {
            var query = dbSet
                .AsNoTracking()
                .Where(x => !x.IsDeleted);
                //.Include(x => x.Category)
                //.Include(x => x.Brand)
                //.Include(x => x.Supplier)
                //.Include(x => x.Origin);

            var totalCount = await query.CountAsync();
            var activeCount = await query.CountAsync(x => x.IsActive);
            var outOfStockCount = await query.CountAsync(x => x.Stock <= 0);
            //var items = await query
            //    .OrderBy(x => x.Id)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();
            var items = await query
        .Include(x => x.Category)
        .Include(x => x.Brand)
        .Include(x => x.Supplier)
        .Include(x => x.Origin)
        .Include(x => x.Images.Where(i => i.IsMain))
        .OrderBy(x => x.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

            return (items, totalCount,activeCount, outOfStockCount);
        }
    }
}
