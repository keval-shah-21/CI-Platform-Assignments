using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Skill
{
    public short SkillId { get; set; }

    public string SkillName { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
