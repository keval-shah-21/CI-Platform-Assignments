using System.Linq.Expressions;
namespace CI_Platform.DataAccess.Repository.Interface;

public interface IRepository<T> where T: class
{
    public IEnumerable<T> GetAll();
    public Task<IEnumerable<T>> GetAllAsync();

    void Add(T entity);
    Task AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);

    void Update(T entity);
}
