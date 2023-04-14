using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    private readonly ApplicationDbContext _context;
    public ContactRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Contact> GetAllWithInclude()
    {
        return dbSet
            .Include(c => c.User)
            .ToList();
    }
    public Contact GetFirstOrDefaultWithInclude(Expression<Func<Contact, bool>> filter)
    {
        return dbSet
            .Include(c => c.User)
            .FirstOrDefault(filter)!;
    }
    public void RemoveById(long id)
    {
        var idParameter = new SqlParameter("@contactId", id);
        _context.Database.ExecuteSqlRaw("DELETE FROM contact WHERE contact_id = @contactId", idParameter);
    }
}
