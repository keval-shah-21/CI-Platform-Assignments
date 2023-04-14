using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IAdminRepository : IRepository<Admin>
{
    void UpdatePassword(string email, string password);
}
