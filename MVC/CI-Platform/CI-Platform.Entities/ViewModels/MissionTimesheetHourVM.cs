using CI_Platform.Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class MissionTimesheetHourVM
{
    public long TimesheetId { get; set; }

    [Required]
    public long? MissionId { get; set; }

    public long? UserId { get; set; }

    [Required]
    [Display(Name ="Date Volunteered")]
    public DateTime? DateVolunteered { get; set; }

    [Required]
    [Range(0, 24)]
    [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Hours input.")]
    public int? Hours { get; set; }

    [Required]
    [Range(0, 60)]
    [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Minutes input.")]
    public int? Minutes { get; set; }

    [Required]
    [Display(Name = "Message")]
    [StringLength(80)]
    public string? Notes { get; set; }
    public ApprovalStatus? ApprovalStatus { get; set; }

    public List<MissionVM>? MissionVM { get; set; }
}
