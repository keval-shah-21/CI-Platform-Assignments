namespace CI_Platform.Entities.ViewModels;

public class MissionApplicationVM
{
    public long MissionApplicationId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public DateTime AppliedAt { get; set; }

    public byte ApprovalStatus { get; set; }
}
