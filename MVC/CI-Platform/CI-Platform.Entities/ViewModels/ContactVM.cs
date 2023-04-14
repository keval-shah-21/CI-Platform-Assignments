using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class ContactVM
{
    public long ContactId { get; set; }

    [Required]
    [StringLength(100)]
    public string Subject { get; set; } = null!;

    [Required]
    public string? Message { get; set; }

    public bool? Status { get; set; }

    public long? UserId { get; set; }

    public string? Reply { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public UserVM? User { get; set; }
}
