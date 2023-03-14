using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Mission
{
    public long MissionId { get; set; }

    public int MissionCity { get; set; }

    public short MissionCountry { get; set; }

    public short MissionThemeId { get; set; }

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string OrganizationName { get; set; } = null!;

    public string? OrganizationDetails { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool MissionType { get; set; }

    public short? TotalSeats { get; set; }

    public DateTime? RegistrationDeadline { get; set; }

    public byte Availability { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public byte? MissionRating { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<FavouriteMission> FavouriteMissions { get; } = new List<FavouriteMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual City MissionCityNavigation { get; set; } = null!;

    public virtual Country MissionCountryNavigation { get; set; } = null!;

    public virtual ICollection<MissionDocument> MissionDocuments { get; } = new List<MissionDocument>();

    public virtual ICollection<MissionGoal> MissionGoals { get; } = new List<MissionGoal>();

    public virtual ICollection<MissionMedium> MissionMedia { get; } = new List<MissionMedium>();

    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();

    public virtual MissionTheme MissionTheme { get; set; } = null!;

    public virtual ICollection<Story> Stories { get; } = new List<Story>();
}
