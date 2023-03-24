﻿using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Story
{
    public long StoryId { get; set; }

    public long MissionId { get; set; }

    public long UserId { get; set; }

    public string Title { get; set; } = null!;

    public DateTimeOffset? PublishedAt { get; set; }

    public string Description { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public byte ApprovalStatus { get; set; }

    public string? VideoUrl { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual ICollection<StoryInvite> StoryInvites { get; } = new List<StoryInvite>();

    public virtual ICollection<StoryMedium> StoryMedia { get; } = new List<StoryMedium>();

    public virtual User User { get; set; } = null!;
}
