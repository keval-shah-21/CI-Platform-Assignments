namespace CI_Platform.Entities.DataModels;

public partial class Notification
{
    public long NotificationId { get; set; }

    public string? Message { get; set; }

    public byte? NotificationType { get; set; }
}
