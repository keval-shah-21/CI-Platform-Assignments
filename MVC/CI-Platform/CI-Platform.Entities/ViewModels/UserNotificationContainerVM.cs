namespace CI_Platform.Entities.ViewModels;

public class UserNotificationContainerVM
{
    public IEnumerable<UserNotificationVM> NewNotificationVMs { get; set; } = new List<UserNotificationVM>();
    public IEnumerable<UserNotificationVM> OldNotificationVMs { get; set; } = new List<UserNotificationVM>();
    public NotificationSettingVM NotificationSettingVM { get; set; } = new();
    public DateTimeOffset? LastCheck { get; set; }
    public int? Unread { get; set; }
}
