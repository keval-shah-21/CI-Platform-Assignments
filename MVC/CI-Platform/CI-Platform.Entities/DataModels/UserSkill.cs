using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class UserSkill
{
    public long UserSkillId { get; set; }

    public long UserId { get; set; }

    public short SkillId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
