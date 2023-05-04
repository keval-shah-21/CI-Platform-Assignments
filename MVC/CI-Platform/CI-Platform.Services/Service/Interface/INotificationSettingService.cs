using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface INotificationSettingService
{
    Task<NotificationSettingVM> GetNotificationSettingByUserId(long userId);
    Task<IEnumerable<NotificationSettingVM>> GetAllAsync();
    Task UpdateNotificationSetting(NotificationSettingVM setting);
    Task Add(long userId);
}
