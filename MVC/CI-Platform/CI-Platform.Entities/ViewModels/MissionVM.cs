namespace CI_Platform.Entities.ViewModels;

public class MissionVM{
    public long MissionId { get; set; }

    public int MissionCityId { get; set; }
    public string MissionCity{get; set;} = string.Empty;

    public short MissionCountryId { get; set; }
    public string MissionCountry{get; set;} = string.Empty;

    public short MissionThemeId { get; set; }
    public string MissionThemeName{get; set;} = string.Empty;

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string OrganizationName { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool MissionType { get; set; }

    public short? TotalSeats { get; set; }

    public DateTime? RegistrationDeadline { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public byte? MissionRating { get; set; }

    public string MissionMedia {get; set;} = string.Empty;

    public List<FavouriteMissionVM>? FavouriteMissionVM {get; set;}

    public List<MissionApplicationVM>? MissionApplicationVM {get; set;}

    public MissionGoalVM? MissionGoalVM{get; set;}

    public List<MissionSkillVM>? MissionSkillVM {get; set;}
}