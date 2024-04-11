using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestingEFCoreApp.Data;

namespace TestingEFCoreApp.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly EFCoreAppContext _appContext;

        protected BaseRepository(EFCoreAppContext appContext)
        {
            _appContext = appContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _appContext.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _appContext.Set<T>().AddAsync(entity);
            await _appContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _appContext.Entry(entity).State = EntityState.Modified;
            await _appContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _appContext.Set<T>().FindAsync(id);
            _appContext.Set<T>().Remove(entity);
            await _appContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> condition)
        {
            return await _appContext.Set<T>().Where(condition).ToListAsync();
        }

    }
}
