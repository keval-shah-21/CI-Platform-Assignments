namespace CI_Platform.Entities.DataModels;

public partial class NotificationSetting
{
    public long UserId { get; set; }

    public bool RecommendMission { get; set; }

    public bool RecommendStory { get; set; }

    public bool VolunteeringHour { get; set; }

    public bool VolunteeringGoal { get; set; }

    public bool Comment { get; set; }

    public bool MyStory { get; set; }

    public bool NewMission { get; set; }

    public bool NewMessage { get; set; }

    public bool MissionApplication { get; set; }

    public bool News { get; set; }

    public bool Email { get; set; }

    public virtual User User { get; set; } = null!;
}
