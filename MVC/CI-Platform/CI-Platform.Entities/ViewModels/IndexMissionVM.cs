using CI_Platform.Entities.Constants;

namespace CI_Platform.Entities.ViewModels;

public class IndexMissionVM
{
    public long MissionId { get; set; }

    public CityVM CityVM { get; set; } = null!;

    public CountryVM CountryVM { get; set; } = null!;

    public MissionThemeVM MissionThemeVM { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }
    public string? Description { get; set; }

    public string OrganizationName { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public MissionType MissionType { get; set; }

    public short? TotalSeats { get; set; }

    public short? SeatsLeft { get; set; }

    public MissionStatus Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegistrationDeadline { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public byte? MissionRating { get; set; }

    public Availability Availability { get; set; }


    public string MissionThumbnail { get; set; } = string.Empty;

    public List<FavouriteMissionVM>? FavouriteMissionVM { get; set; } = new();

    public List<MissionApplicationVM>? MissionApplicationVM { get; set; } = new();

    public MissionGoalVM? MissionGoalVM { get; set; } = new();

    public List<MissionSkillVM>? MissionSkillVM { get; set; } = new();
}
