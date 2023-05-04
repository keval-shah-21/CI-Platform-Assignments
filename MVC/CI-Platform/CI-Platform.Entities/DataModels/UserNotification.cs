namespace CI_Platform.Entities.DataModels;

public partial class UserNotification
{
    public long UserId { get; set; }

    public long NotificationId { get; set; }

    public bool? IsRead { get; set; }

    public string? FromUserAvatar { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual Notification Notification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
