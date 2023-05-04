using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels;

public class NotificationVM
{
    public long NotificationId { get; set; }

    public string? Message { get; set; }

    public NotificationType? NotificationType { get; set; }

}
