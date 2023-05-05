using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface INotificationSettingRepository : IRepository<NotificationSetting>
{
    Task<IEnumerable<NotificationSetting>> GetAllWithIncludeAsync();
    Task<NotificationSetting> GetFirstOrDefaultWithIncludeAsync(Expression<Func<NotificationSetting, bool>> filter);
}
