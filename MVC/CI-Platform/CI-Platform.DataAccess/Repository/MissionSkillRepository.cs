using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionSkillRepository : Repository<MissionSkill>, IMissionSkillRepository
{
    public MissionSkillRepository(ApplicationDbContext context) : base(context)
    {
    }
}
