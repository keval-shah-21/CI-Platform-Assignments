namespace CI_Platform.Entities.ViewModels;

public class MissionRatingVM
{
    public long MissionRatingId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public byte Rating { get; set; }
}
