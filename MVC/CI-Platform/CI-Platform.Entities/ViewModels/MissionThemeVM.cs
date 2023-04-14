using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class MissionThemeVM
{
    public short MissionThemeId { get; set; }

    [Required]
    [Display(Name = "Theme Title")]
    public string? MissionThemeName { get; set; }

    [Required]
    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
