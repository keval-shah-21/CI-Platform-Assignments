using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class UserNotificationRepository : Repository<UserNotification>, IUserNotificationRepository
{
    private readonly ApplicationDbContext _context;
    public UserNotificationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<UserNotification>> GetAllWithIncludeAsync()
    {
        return await dbSet
            .Include(x => x.Notification)
            .ToListAsync();
    }
    public async Task ClearAllNotification(long userId)
    {
        SqlParameter idParameter = new SqlParameter("@id", userId);
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM user_notification WHERE user_id = @id", idParameter);
    }
    public async Task MarkAsReadNotification(long userNotificationId)
    {
        SqlParameter idParameter = new SqlParameter("@id", userNotificationId);
        SqlParameter dateParameter = new SqlParameter("@date", DateTimeOffset.Now);
        await _context.Database.ExecuteSqlRawAsync("UPDATE user_notification SET is_read = 1, updated_at = @date WHERE user_notification_id = @id", dateParameter, idParameter);
    }
}
