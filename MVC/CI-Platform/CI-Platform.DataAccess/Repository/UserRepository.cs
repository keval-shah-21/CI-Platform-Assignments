using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context):base(context)
    {
    }

    public List<User> GetAllToRecommendMission()
    {
        return dbSet
        .Include(u => u.MissionInviteFromUsers)
        .Include(u => u.MissionInviteToUsers)
        .ToList();
    }
    public List<User> GetAllToRecommendStory()
    {
        return dbSet
        .Include(u => u.StoryInviteFromUsers)
        .Include(u => u.StoryInviteToUsers)
        .ToList();
    }
}