using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Repositories.EFRepositories
{
    public class EFCategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryDAL
    {
        public async Task<IEnumerable<Category>> GetAllWithParentAsync()
        {
            return await context.Categories
        .Include(x => x.ParentCategory)
        .ToListAsync();
        }

        public async Task<Category?> GetByIdWithParentAsync(int id)
        {
            return await context.Categories
        .Include(x => x.ParentCategory)
        .FirstOrDefaultAsync(x => x.Id == id);
        }
        
    }
}
