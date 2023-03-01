using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class MissionSkill
{
    public long MissionSkillId { get; set; }

    public short SkillId { get; set; }

    public long MissionId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
