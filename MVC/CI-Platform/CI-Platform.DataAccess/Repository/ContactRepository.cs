using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<Contact> GetAllWithInclude()
    {
        return dbSet
            .Include(c => c.User)
            .ToList();
    }
}
