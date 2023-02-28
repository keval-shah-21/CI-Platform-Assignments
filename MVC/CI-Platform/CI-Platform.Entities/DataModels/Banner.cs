using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Banner
{
    public int BannerId { get; set; }

    public string BannerImage { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public byte? SortOrder { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
