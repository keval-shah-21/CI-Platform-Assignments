using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface INotificationSettingService
{
    Task<NotificationSettingVM> GetByUserId(long userId);
    Task<IEnumerable<NotificationSettingVM>> GetAllToSendNotification(string settingType);
    Task<NotificationSettingVM> GetByUserIdToSendNotification(long? id, string settingType);
    Task UpdateNotificationSetting(NotificationSettingVM setting);
    Task Add(long userId);
    Task<IEnumerable<NotificationSettingVM>> GetAllToSendRecommendNotification(List<long> toUsers, string settingType);
}
