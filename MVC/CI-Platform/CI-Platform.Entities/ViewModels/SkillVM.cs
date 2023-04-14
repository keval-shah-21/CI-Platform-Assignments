using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class SkillVM
{
    public short SkillId { get; set; }

    [Required]
    [Display(Name = "Skill Title")]
    public string SkillName { get; set; } = null!;

    [Required]
    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
