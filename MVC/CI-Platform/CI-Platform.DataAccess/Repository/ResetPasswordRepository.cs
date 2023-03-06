using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;

namespace CI_Platform.DataAccess.Repository;

public class ResetPasswordRepository: Repository<ResetPassword>, IResetPasswordRepository
{
    public ResetPasswordRepository(ApplicationDbContext context):base(context)
    {
    }
}
