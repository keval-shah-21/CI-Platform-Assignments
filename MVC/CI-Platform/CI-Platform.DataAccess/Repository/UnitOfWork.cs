using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
namespace CI_Platform.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        User = new UserRepository(_context);
    }
    public IUserRepository User{get; private set;}

    public void Save()
    {
        _context.SaveChanges();
    }
}