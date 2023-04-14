using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class VerifyEmailRepository : Repository<VerifyEmail>, IVerifyEmailRepository
{
    public VerifyEmailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
