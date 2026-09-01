using ErenMotorWeb.Entities;
using System.Linq.Expressions;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IGenericDAL<T>
        where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity);

        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter);

        T? GetById(int id);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByFilter(
            Expression<Func<T, bool>> filter);

        IQueryable<T> QueryWithIncludes(
            params Expression<Func<T, object>>[] includeProperties);
    }
}