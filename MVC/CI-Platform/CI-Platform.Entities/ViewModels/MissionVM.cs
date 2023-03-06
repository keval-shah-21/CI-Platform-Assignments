namespace CI_Platform.Entities.ViewModels;

public class MissionVM{
    public long MissionId { get; set; }

    public int MissionCity { get; set; }

    public short MissionCountry { get; set; }

    public short MissionThemeId { get; set; }

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
}