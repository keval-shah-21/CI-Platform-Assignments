using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class MissionTheme
{
    public short MissionThemeId { get; set; }

    public string? MissionThemeName { get; set; }

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
}
