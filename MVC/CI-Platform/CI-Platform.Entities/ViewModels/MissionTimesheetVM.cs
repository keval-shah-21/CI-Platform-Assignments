namespace CI_Platform.Entities.ViewModels;

public class MissionTimesheetVM
{
    public long TimesheetId { get; set; }

    public long? MissionId { get; set; }

    public long? UserId { get; set; }

    public DateTime? DateVolunteered { get; set; }

    public TimeSpan? TimeVolunteered { get; set; }

    public string? Notes { get; set; }

    public int? Action { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public MissionVM? Mission { get; set; }

    public UserVM? User { get; set; }
}
