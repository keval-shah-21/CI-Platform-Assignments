namespace CI_Platform.Entities.ViewModels;

public class UserNotificationVM
{
    public long UserNotificationId { get; set; }
    public long UserId { get; set; }

    public long NotificationId { get; set; }

    public bool? IsRead { get; set; }

    public string? FromUserAvatar { get; set; }

    public NotificationVM? NotificationVM { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}
