using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository;

public class NotificationSettingRepository : Repository<NotificationSetting>, INotificationSettingRepository
{
    public NotificationSettingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<NotificationSetting>> GetAllWithIncludeAsync()
    {
        return await dbSet
            .Include(n => n.User)
            .ToListAsync();
    }
    public async Task<NotificationSetting> GetFirstOrDefaultWithIncludeAsync(Expression<Func<NotificationSetting, bool>> filter)
    {
        return await dbSet
            .Include(n => n.User)
            .FirstOrDefaultAsync(filter);
    }
}
