using CI_Platform.Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class GoalMissionVM
{
    public long MissionId { get; set; }

    [Required]
    [Display(Name = "City")]
    public int MissionCity { get; set; }

    [Required]
    [Display(Name = "Country")]
    public short MissionCountry { get; set; }

    [Required]
    [Display(Name = "Mission Theme")]
    public short MissionThemeId { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    [Display(Name = "Short Description")]
    public string? ShortDescription { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Organization Name")]
    public string OrganizationName { get; set; } = null!;

    [Display(Name = "Organization Details")]
    public string? OrganizationDetails { get; set; }

    [Required]
    [Display(Name = "Start Date")]
    public DateTime? StartDate { get; set; }

    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Total Seats")]
    public short? TotalSeats { get; set; }

    [Display(Name = "Registration Deadline")]
    public DateTime? RegistrationDeadline { get; set; }

    [Required]
    public Availability Availability { get; set; }

    public long MissionGoalId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Goal Objective")]
    public string? GoalObjective { get; set; }

    [Required]
    [Display(Name = "Goal Value")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Action input.")]
    public int? GoalValue { get; set; }

    [Required]
    [Display(Name = "Status")]
    public bool? IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public List<CityVM> CityVMs { get; set; } = new();
    public List<CountryVM> CountryVMs { get; set; } = new();
    public List<MissionThemeVM> ThemeVMs { get; set; } = new();
    public List<SkillVM> SkillVMs { get; set; } = new();
    public List<MissionMediaVM> MissionMediaVM { get; set; } = new();
    public List<MissionDocumentVM> MissionDocumentVM { get; set; } = new();
    public List<MissionSkillVM> MissionSkillVM { get; set; } = new();
}
