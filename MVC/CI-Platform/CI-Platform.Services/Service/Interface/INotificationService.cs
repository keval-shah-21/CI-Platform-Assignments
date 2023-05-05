using CI_Platform.Entities.Constants;

namespace CI_Platform.Services.Service.Interface;

public interface INotificationService
{
    Task SendNotificationToAllUsers(string message, NotificationType type, string settingType);
    Task SendUserNotification(string message, NotificationType type, long userId, string settingType, string? avatar);
}
