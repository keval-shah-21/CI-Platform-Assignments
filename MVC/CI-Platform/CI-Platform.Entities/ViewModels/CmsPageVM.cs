using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class CmsPageVM
{
    public long CmsPageId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 20, ErrorMessage = "Title should be between 20 to 255 characters.")]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    [MaxLength(30)]
    public string Slug { get; set; } = null!;

    [Required]
    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
