using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionRepository : Repository<Mission>, IMissionRepository
{
    public MissionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
