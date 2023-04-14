using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class SkillRepository: Repository<Skill>, ISkillRepository
{
    private readonly ApplicationDbContext _context;
    public SkillRepository(ApplicationDbContext context): base(context)
    {
        _context = context;
    }
    public void RemoveById(long id)
    {
        var idParameter = new SqlParameter("@skillId", id);
        _context.Database.ExecuteSqlRaw("DELETE FROM skill WHERE skill_id = @skillId", idParameter);
    }

    public bool IsAlreadyUsed(long id)
    {
        var missionResult = _context.Missions.FromSqlInterpolated($"select * from mission_skill where skill_id = {id}");
        var userResult = _context.Missions.FromSqlInterpolated($"select * from user_skill where skill_id = {id}");
        return missionResult.Count() > 0 || userResult.Count()> 0;
    }
}
