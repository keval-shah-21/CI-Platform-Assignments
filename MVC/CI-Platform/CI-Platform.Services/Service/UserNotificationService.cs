using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class UserNotificationService : IUserNotificationService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserNotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<UserNotificationVM>> GetAllByUserId(long userId)
    {
        var notifs = await _unitOfWork.UserNotification.GetAllWithIncludeAsync();
        return notifs
            .Where(n => n.UserId == userId)
            .Select(ConvertUserNotificationToVM);
    }
    public async Task<DateTimeOffset> GetLastCheckByUserId(long userId)
    {
        var lastCheck = await _unitOfWork.NotificationCheck.GetFirstOrDefaultAsync(n => n.UserId == userId);
        return lastCheck.LastCheck;
    }
    public async Task AddNotificationCheck(long userId)
    {
        await _unitOfWork.NotificationCheck.AddAsync(new NotificationCheck()
        {
            UserId = userId,   
            LastCheck = DateTimeOffset.Now
        });
    }
    public async Task UpdateLastCheck(long userId) => await _unitOfWork.NotificationCheck.UpdateLastCheck(userId);
    public async Task ClearAllNotification(long userId) => await _unitOfWork.UserNotification.ClearAllNotification(userId);
    public async Task MarkAsReadNotification(long notificationId) => await _unitOfWork.UserNotification.MarkAsReadNotification(notificationId);
    public static UserNotificationVM ConvertUserNotificationToVM(UserNotification notification)
    {
        return new UserNotificationVM()
        {
            IsRead = notification.IsRead,
            FromUserAvatar = notification.FromUserAvatar,
            UserId = notification.UserId,
            NotificationId = notification.NotificationId,
            LastModified = notification.UpdatedAt ?? notification.CreatedAt,
            NotificationVM = ConvertNotificationToVM(notification.Notification)
        };
    }
    public static NotificationVM ConvertNotificationToVM(Notification notification)
    {
        return new NotificationVM()
        {
            Message = notification.Message,
            NotificationId = notification.NotificationId,
            NotificationType = (NotificationType)notification.NotificationType!
        };
    }

    public async Task SendNotifications()
    {

    }
}
