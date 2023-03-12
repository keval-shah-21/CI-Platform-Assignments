using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class MissionRepository : Repository<Mission>, IMissionRepository
{
    private readonly ApplicationDbContext _context;
    public MissionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Mission> GetAllMissions()
    {   
        return _context.Missions
        .Include(m => m.MissionMedia)
        .Include(m => m.MissionApplications)
        .Include(m => m.MissionGoals)
        .Include(m => m.MissionSkills)
        .Include(m => m.MissionTheme)
        .Include(m => m.FavouriteMissions)
        .Include(m => m.MissionCityNavigation)
        .Include(m => m.MissionCountryNavigation);
    }
}
