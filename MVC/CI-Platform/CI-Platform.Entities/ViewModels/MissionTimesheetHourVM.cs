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
    [MaxLength(24)]
    [MinLength(0)]
    public byte? Hours;

    [Required]
    [MaxLength(60)]
    [MinLength(0)]
    public byte? Minutes;

    [Required]
    [Display(Name = "Message")]
    public string? Notes { get; set; }
}
