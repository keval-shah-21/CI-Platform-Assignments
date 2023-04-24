using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository;

public class MissionApplicationRepository : Repository<MissionApplication>, IMissionApplicationRepository
{
    private readonly ApplicationDbContext _context;
    public MissionApplicationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context; 
    }

    public IEnumerable<MissionApplication> GetAllForStoryMissions()
    {
        return dbSet
            .Include(mi => mi.Mission);
    }
    public void CancelMission(long missionId, long userId)
    {
        var missionIdParameter = new SqlParameter("@missionId", missionId);
        var userIdParameter = new SqlParameter("@userId", userId);

        _context.Database.ExecuteSqlRaw("DELETE FROM mission_application where user_id = @userId AND mission_id = @missionId", missionIdParameter, userIdParameter);
    }
    public IEnumerable<MissionApplication> GetAllWithInclude()
    {
        return dbSet
            .Include(mi => mi.Mission)
            .Include(mi => mi.User)
            .ToList();
    }
    public IEnumerable<MissionApplication> GetAllAdmin()
    {
        return dbSet
            .Include(m => m.Mission)
            .Include(m => m.User)
                .ThenInclude(u => u.City)
            .Include(m => m.User)
                .ThenInclude(u => u.Country)
            .Include(m => m.User)
                .ThenInclude(u => u.UserSkills)
                .ThenInclude(u => u.Skill)
                .ToList();
    }
    public MissionApplication GetFirstOrDefaultWithInclude(Expression<Func<MissionApplication, bool>> filter)
    {
        return dbSet
            .Include(m => m.Mission)
            .Include(m => m.User)
                .ThenInclude(u => u.City)
            .Include(m => m.User)
                .ThenInclude(u => u.Country)
            .Include(m => m.User)
                .ThenInclude(u => u.UserSkills)
                .ThenInclude(u => u.Skill)
            .FirstOrDefault(filter)!;
    }


    public void UpdateStatus(long id, byte value)
    {
        SqlParameter idParameter = new SqlParameter("@appId", id);
        SqlParameter statusParameter = new SqlParameter("@status", value);

        _context.Database.ExecuteSqlRaw("UPDATE mission_application SET approval_status = @status WHERE mission_application_id = @appId", statusParameter, idParameter);
    }
}
