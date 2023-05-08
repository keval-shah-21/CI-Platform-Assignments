using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface INotificationSettingRepository : IRepository<NotificationSetting>
{
    Task<IEnumerable<NotificationSetting>> GetAllWithIncludeAsync();
    Task<IEnumerable<NotificationSetting>> GetAllToSendNotification(string settingType);
    Task<NotificationSetting?> GetByUserIdToSendNotification(long? id, string settingType);
    Task<NotificationSetting> GetFirstOrDefaultWithIncludeAsync(Expression<Func<NotificationSetting, bool>> filter);
    Task<IEnumerable<NotificationSetting?>> GetAllToSendRecommendNotification(List<long> toUsers, string settingType);
}
