using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class MissionThemeRepository : Repository<MissionTheme>, IMissionThemeRepository
{
    private readonly ApplicationDbContext _context;
    public MissionThemeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void RemoveById(long id)
    {
        var idParameter = new SqlParameter("@themeId", id);
        _context.Database.ExecuteSqlRaw("DELETE FROM mission_theme WHERE mission_theme_id = @themeId", idParameter);
    }

    public bool IsAlreadyUsed(long id)
    {
        var result = _context.Missions.FromSqlInterpolated($"select * from mission where mission_theme_id = {id}");
        return result.Count() > 0;
    }
}
