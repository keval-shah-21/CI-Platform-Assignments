using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IContactRepository: IRepository<Contact>
{
    IEnumerable<Contact> GetAllWithInclude();
}
