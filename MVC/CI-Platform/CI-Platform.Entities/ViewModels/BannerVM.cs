using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public partial class BannerVM
{
    public long BannerId { get; set; }

    public string? MediaName { get; set; }

    public string? MediaType { get; set; }

    public string? MediaPath { get; set; }

    [Required]
    [Display(Name="Sort Order")]
    [RegularExpression(@"^-?\d+$", ErrorMessage ="Only numbers are allowed.")]
    public int? SortOrder { get; set; }

    [Required]
    [StringLength(200)]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
