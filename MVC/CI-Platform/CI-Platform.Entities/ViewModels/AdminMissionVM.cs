using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels;

public class AdminMissionVM
{
    public long MissionId { get; set; }

    public string? CityName { get; set; }

    public string? CountryName { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public MissionType MissionType { get; set; }

    public bool? IsActive { get; set; }

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
