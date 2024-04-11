using System.Linq.Expressions;

namespace TestingEFCoreApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> condition);
    }
}
