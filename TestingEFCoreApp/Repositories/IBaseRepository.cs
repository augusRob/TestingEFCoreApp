using System.Linq.Expressions;

namespace TestingEFCoreApp.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);

        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> condition);
    }
}
