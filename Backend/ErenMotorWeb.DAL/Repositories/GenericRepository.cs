using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ErenMotorWeb.DAL.Repositories
{
    public class GenericRepository<T>(AppDbContext context)
        : IGenericDAL<T>
        where T : class, IEntityBase, new()
    {
        protected readonly DbSet<T> dbSet = context.Set<T>();

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<T> QueryWithIncludes(
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            return includeProperties.Aggregate(
                query,
                (current, includeProperty)
                    => current.Include(includeProperty));
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity is null)
                return;

            dbSet.Remove(entity);

            await SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return [.. dbSet.AsNoTracking()];
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter)
        {
            return await dbSet
                .AsNoTracking()
                .Where(filter)
                .ToListAsync();
        }

        public T? GetById(int id)
        {
            return dbSet
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> GetByFilter(
            Expression<Func<T, bool>> filter)
        {
            return await dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(filter);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
        }
    }
}