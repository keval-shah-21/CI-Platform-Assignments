using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IUserNotificationRepository: IRepository<UserNotification>
{
    Task<IEnumerable<UserNotification>> GetAllWithIncludeAsync();
    Task ClearAllNotification(long userId);
    Task MarkAsReadNotification(long notificationId);
}
