using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository;

public class MissionRepository : Repository<Mission>, IMissionRepository
{
    public MissionRepository(ApplicationDbContext context) : base(context)
    {
    }
    public override IEnumerable<Mission> GetAll()
    {
        return dbSet
        .Include(m => m.MissionMedia)
        .Include(m => m.MissionApplications)
            .ThenInclude(ma => ma.User)
        .Include(m => m.MissionGoals)
        .Include(m => m.MissionSkills)
            .ThenInclude(ms => ms.Skill)
        .Include(m => m.MissionTheme)
        .Include(m => m.FavouriteMissions)
        .Include(m => m.MissionCityNavigation)
        .Include(m => m.MissionCountryNavigation)
        .ToList();
    }

    public override Mission GetFirstOrDefault(Expression<Func<Mission, bool>> filter)
    {
        return dbSet
        .Include(m => m.MissionMedia)
        .Include(m => m.MissionApplications)
            .ThenInclude(ma => ma.User)
        .Include(m => m.MissionGoals)
        .Include(m => m.MissionSkills)
            .ThenInclude(ms => ms.Skill)
        .Include(m => m.MissionTheme)
        .Include(m => m.FavouriteMissions)
        .Include(m => m.MissionCityNavigation)
        .Include(m => m.MissionCountryNavigation)
        .Include(m => m.Comments)
            .ThenInclude(c => c.User)
        .Include(m => m.MissionRatings)
        .Include(m => m.MissionDocuments)
        .FirstOrDefault(filter)!;
    }
}