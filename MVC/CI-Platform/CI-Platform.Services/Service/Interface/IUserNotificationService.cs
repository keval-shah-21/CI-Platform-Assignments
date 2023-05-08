using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IUserNotificationService
{
    Task<IEnumerable<UserNotificationVM>> GetAllByUserId(long userId);
    Task<DateTimeOffset> GetLastCheckByUserId(long userId);
    Task UpdateLastCheck(long userId);
    Task AddNotificationCheck(long userId);
    Task ClearAllNotification(long userId);
    Task MarkAsReadNotification(long userNotificationId);
}
