using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class CmsPage
{
    public short CmsPageId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Slug { get; set; }

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
