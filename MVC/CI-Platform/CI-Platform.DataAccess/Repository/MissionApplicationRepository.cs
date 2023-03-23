using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class MissionApplicationRepository : Repository<MissionApplication>, IMissionApplicationRepository
{
    public MissionApplicationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<MissionApplication> GetAllForStoryMissions()
    {
        return dbSet
            .Include(mi => mi.Mission);
    }
}
