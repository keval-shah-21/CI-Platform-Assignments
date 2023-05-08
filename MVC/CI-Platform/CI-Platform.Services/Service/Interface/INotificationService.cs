using CI_Platform.Entities.Constants;
using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface INotificationService
{
    Task SendNotificationToAllUsers(SendNotificationVM sendNotificationVM, List<long> toUsers);
    Task SendUserNotification(SendNotificationVM sendNotificationVM);
}
