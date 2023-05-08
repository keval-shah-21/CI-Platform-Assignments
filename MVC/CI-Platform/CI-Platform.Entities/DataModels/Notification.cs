namespace CI_Platform.Entities.DataModels;

public partial class Notification
{
    public long NotificationId { get; set; }

    public string? Message { get; set; }

    public byte? NotificationType { get; set; }

    public byte? SettingType { get; set; }

    public virtual ICollection<UserNotification> UserNotifications { get; } = new List<UserNotification>();
}
