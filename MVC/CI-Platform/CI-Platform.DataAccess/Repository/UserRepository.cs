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
    public void UpdatePassword(string email, string password)
    {
        var emailParameter = new SqlParameter("@email", email);
        var passwordParameter = new SqlParameter("@password", password);

        _context.Database.ExecuteSqlRaw("UPDATE [user] SET password = @password WHERE email = @email",passwordParameter, emailParameter);
    }
}