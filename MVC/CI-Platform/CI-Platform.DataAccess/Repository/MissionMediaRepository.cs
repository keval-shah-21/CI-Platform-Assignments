using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionMediaRepository : Repository<MissionMedia>, IMissionMediaRepository
{
    public MissionMediaRepository(ApplicationDbContext context) : base(context)
    {
    }
}
