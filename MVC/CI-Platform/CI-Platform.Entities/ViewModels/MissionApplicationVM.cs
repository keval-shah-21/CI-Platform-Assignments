using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels;

public class MissionApplicationVM
{
    public long MissionApplicationId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public DateTime AppliedAt { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }

    public string? UserName { get; set; }

    public string? Avatar { get; set; }
}