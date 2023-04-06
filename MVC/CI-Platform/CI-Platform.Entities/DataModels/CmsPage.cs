using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class CmsPage
{
    public long CmsPageId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string Slug { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
