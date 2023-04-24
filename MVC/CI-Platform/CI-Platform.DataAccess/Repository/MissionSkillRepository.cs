using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class MissionSkillRepository : Repository<MissionSkill>, IMissionSkillRepository
{
    private readonly ApplicationDbContext _context;
    public MissionSkillRepository(ApplicationDbContext context) : base(context)
    {
        _context = context; 
    }
    public void RemoveMissionSkill(string skillId, long missionId) {
        SqlParameter skillParamter = new SqlParameter("@skill", skillId);
        SqlParameter missionParamter = new SqlParameter("@mission", missionId);

        _context.Database.ExecuteSqlRaw("DELETE FROM mission_skill where mission_id = @mission AND skill_id = @skill", missionParamter, skillParamter);
    }
}
