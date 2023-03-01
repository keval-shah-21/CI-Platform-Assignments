using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public short CountryId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
