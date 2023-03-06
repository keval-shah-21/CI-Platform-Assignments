using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionApplicationRepository : Repository<MissionApplication>, IMissionApplicationRepository
{
    public MissionApplicationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
