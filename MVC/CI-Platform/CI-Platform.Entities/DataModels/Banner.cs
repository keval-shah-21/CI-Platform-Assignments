using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Banner
{
    public long BannerId { get; set; }

    public string? MediaName { get; set; }

    public string? MediaType { get; set; }

    public string? MediaPath { get; set; }

    public int? SortOrder { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
