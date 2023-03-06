using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
namespace CI_Platform.DataAccess.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context):base(context)
    {
    }
}