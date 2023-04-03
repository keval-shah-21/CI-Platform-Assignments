using CI_Platform.Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class MissionTimesheetGoalVM
{
    public long TimesheetId { get; set; }

    [Required]
    public long? MissionId { get; set; }

    public long? UserId { get; set; }

    [Required]
    [Display(Name = "Date Volunteered")]
    public DateTime? DateVolunteered { get; set; }

    [Required]
    [Display(Name ="Message")]
    public string? Notes { get; set; }

    [Required]
    [Display(Name = "Actions")]
    public int? Action { get; set; }

    public ApprovalStatus? ApprovalStatus { get; set; }
}
