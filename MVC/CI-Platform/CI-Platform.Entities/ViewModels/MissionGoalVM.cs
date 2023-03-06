namespace CI_Platform.Entities.ViewModels;

public class MissionGoalVM
{
    public long MissionGoalId { get; set; }

    public long? MissionId { get; set; }

    public string? GoalObjective { get; set; }

    public int? GoalValue { get; set; }

    public int? GoalAchieved { get; set; }
}
