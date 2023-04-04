using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels;

public class MissionTimesheetVM
{
    public long TimesheetId { get; set; }

    public long? MissionId { get; set; }

    public string? MissionName { get; set; }

    public MissionType? MissionType { get; set; } 

    public long? UserId { get; set; }

    public DateTime? DateVolunteered { get; set; }

    public TimeSpan? TimeVolunteered { get; set; }

    public string? Notes { get; set; }

    public int? Action { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public ApprovalStatus? ApprovalStatus { get; set; }

    public UserVM? User { get; set; }
}
