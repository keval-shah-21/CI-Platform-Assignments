using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class MissionTimesheet
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

    public byte? ApprovalStatus { get; set; }

    public virtual Mission? Mission { get; set; }

    public virtual User? User { get; set; }
}
