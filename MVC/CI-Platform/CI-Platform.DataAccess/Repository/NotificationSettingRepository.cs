using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository;

public class NotificationSettingRepository : Repository<NotificationSetting>, INotificationSettingRepository
{
    private readonly ApplicationDbContext _context;
    public NotificationSettingRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationSetting>> GetAllWithIncludeAsync()
    {
        return await dbSet
            .Include(n => n.User)
            .ToListAsync();
    }
    public async Task<NotificationSetting?> GetFirstOrDefaultWithIncludeAsync(Expression<Func<NotificationSetting, bool>> filter)
    {
        return await dbSet
            .Include(n => n.User)
            .FirstOrDefaultAsync(filter);
    }
    public async Task<IEnumerable<NotificationSetting>> GetAllToSendNotification(string settingType)
    {
        return await dbSet.FromSqlRaw($"SELECT * FROM notification_setting WHERE {settingType} = 1")
                       .Include(n => n.User)
                       .ToListAsync();
    }
    public async Task<NotificationSetting?> GetByUserIdToSendNotification(long? id, string settingType)
    {
        return await dbSet.FromSqlRaw($"SELECT * FROM notification_setting WHERE user_id = {id} AND {settingType} = 1")
                           .Include(n => n.User)
                           .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<NotificationSetting?>> GetAllToSendRecommendNotification(List<long> toUsers, string settingType)
    {
        return await dbSet.FromSqlRaw($"SELECT * FROM notification_setting WHERE user_id IN ({string.Join(",", toUsers)})")
                       .Include(n => n.User)
                       .ToListAsync();
    }
}