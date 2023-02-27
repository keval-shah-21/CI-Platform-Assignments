using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Country
{
    public short CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string? Iso { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
