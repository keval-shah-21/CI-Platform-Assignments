using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels;

public class SendNotificationVM
{
    public long? UserId { get; set; }
    public string? FromUserAvatar { get; set; }
    public string? Url { get; set; }
    public string? Href { get; set; }
    public string Message { get; set; } = String.Empty;
    public NotificationType NotificationType { get; set; }
    public NotificationSettingType SettingType { get; set; }
}
