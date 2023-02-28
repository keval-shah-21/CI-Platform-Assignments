using System.Linq.Expressions;
namespace CI_Platform.DataAccess.Repository.Interface;

public interface IRepository<T> where T: class
{
    public IEnumerable<T> GetAll();

    void Add(T entity);

    void Remove(T entity);

    T GetFirstOrDefault(Expression<Func<T, bool>> filter);
}
