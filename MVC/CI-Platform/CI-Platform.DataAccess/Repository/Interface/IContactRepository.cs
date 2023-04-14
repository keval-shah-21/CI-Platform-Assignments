using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IContactRepository: IRepository<Contact>
{
    IEnumerable<Contact> GetAllWithInclude();
    void RemoveById(long id);
    Contact GetFirstOrDefaultWithInclude(Expression<Func<Contact, bool>> filter);
}
