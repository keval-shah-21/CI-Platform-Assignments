using System.Linq.Expressions;
namespace CI_Platform.DataAccess.Repository.Interface;

public interface IRepository<T> where T: class
{
    public IEnumerable<T> GetAll();

    void Add(T entity);
    void AddRange(IEnumerable<T> entities);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    T GetFirstOrDefault(Expression<Func<T, bool>> filter);

    void Update(T entity);

}
