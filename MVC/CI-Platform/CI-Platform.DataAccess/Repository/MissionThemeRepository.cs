using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository;

public class MissionThemeRepository : Repository<MissionTheme>, IMissionThemeRepository
{
    public MissionThemeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
