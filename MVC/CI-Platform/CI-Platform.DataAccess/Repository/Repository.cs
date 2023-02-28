using System.Linq.Expressions;
using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    internal DbSet<T> dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }
    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
}