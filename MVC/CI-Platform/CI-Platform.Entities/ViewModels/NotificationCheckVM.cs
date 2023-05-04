namespace CI_Platform.Entities.ViewModels;

public class NotificationCheckVM
{
    public long UserId { get; set; }

    public DateTimeOffset LastCheck { get; set; }
}
