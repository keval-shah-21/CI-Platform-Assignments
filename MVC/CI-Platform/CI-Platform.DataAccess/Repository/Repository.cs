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

    public virtual IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet.Where(filter);
        return query.FirstOrDefault()!;
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        dbSet.Update(entity);
    }
}