using CI_Platform.Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class ContactVM
{
    public long ContactId { get; set; }

    [Required]
    [StringLength(255)]
    public string Subject { get; set; } = null!;

    [Required]
    public string? Message { get; set; }

    public ApprovalStatus? Status { get; set; }

    public long? UserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public UserVM? User { get; set; }
}
