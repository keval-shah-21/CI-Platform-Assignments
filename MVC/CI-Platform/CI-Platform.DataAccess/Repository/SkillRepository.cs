using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;

namespace CI_Platform.DataAccess.Repository;

public class SkillRepository: Repository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext context): base(context)
    {
    }
}
