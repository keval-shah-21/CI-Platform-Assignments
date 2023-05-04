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
    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }
    public void AddRange(IEnumerable<T> entities)
    {
        dbSet.AddRange(entities);
    }
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await dbSet.AddRangeAsync(entities);
    }
    public virtual IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }
    public virtual T GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet.Where(filter);
        return query.FirstOrDefault()!;
    }
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await dbSet.FirstOrDefaultAsync(filter);

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }

    public void Update(T entity)
    {
        dbSet.Update(entity);
    }
}