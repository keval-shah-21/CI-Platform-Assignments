using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionGoalRepository : Repository<MissionGoal>, IMissionGoalRepository
{
    public MissionGoalRepository(ApplicationDbContext context) : base(context)
    {
    }
}
