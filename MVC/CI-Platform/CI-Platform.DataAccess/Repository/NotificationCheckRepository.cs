using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.DataAccess.Repository;

public class NotificationCheckRepository : Repository<NotificationCheck>, INotificationCheckRepository
{
    private readonly ApplicationDbContext _context;
    public NotificationCheckRepository(ApplicationDbContext context) : base(context)
    {
        _context = context; 
    }

    public async Task UpdateLastCheck(long userId)
    {
        SqlParameter idParameter = new("@id", userId);
        SqlParameter dateParameter = new("@date", DateTimeOffset.Now);

        await _context.Database.ExecuteSqlRawAsync("UPDATE notification_check SET last_check = @date WHERE user_id = @id", dateParameter, idParameter);
    }
}
