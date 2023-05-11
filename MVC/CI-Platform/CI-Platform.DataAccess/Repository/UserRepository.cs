using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context):base(context)
    {
        _context = context;
    }

    public List<User> GetAllToRecommendMission()
    {
        return dbSet
        .Include(u => u.NotificationSetting)
        .Include(u => u.MissionInviteFromUsers)
        .Include(u => u.MissionInviteToUsers)
        .Where(u => u.NotificationSetting.RecommendMission)
        .ToList();
    }
    public List<User> GetAllToRecommendStory()
    {
        return dbSet
        .Include(u => u.NotificationSetting)
        .Include(u => u.StoryInviteFromUsers)
        .Include(u => u.StoryInviteToUsers)
        .Where(u => u.NotificationSetting.RecommendStory)
        .ToList();
    }
    public void UpdatePassword(string email, string password)
    {
        var emailParameter = new SqlParameter("@email", email);
        var passwordParameter = new SqlParameter("@password", password);

        _context.Database.ExecuteSqlRaw("UPDATE [user] SET password = @password WHERE email = @email",passwordParameter, emailParameter);
    }
    public async Task UpdateIsBlockedAsync(string email, int value)
    {
        string query = "UPDATE [user] set is_blocked = {0} where email = {1}";
        await _context.Database.ExecuteSqlRawAsync(query, value, email);
    }
    public async Task UpdateStatusAsync(string email, int value)
    {
        string query = "UPDATE [user] set status = {0} where email = {1}";
        await _context.Database.ExecuteSqlRawAsync(query, value, email);
    }   
}