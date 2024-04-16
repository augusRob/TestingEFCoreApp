using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestingEFCoreApp.Data;

namespace TestingEFCoreApp.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly EFCoreAppContext _appContext;

        protected BaseRepository(EFCoreAppContext appContext)
        {
            _appContext = appContext;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _appContext.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return _appContext.Set<T>().Find(id);
        }

        public virtual void Add(T entity)
        {
            _appContext.Set<T>().Add(entity);
            _appContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _appContext.Entry(entity).State = EntityState.Modified;
            _appContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = _appContext.Set<T>().Find(id);
            _appContext.Set<T>().Remove(entity);
            _appContext.SaveChanges();
        }

        public virtual IEnumerable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _appContext.Set<T>().Where(condition).ToList();
        }

    }
}
