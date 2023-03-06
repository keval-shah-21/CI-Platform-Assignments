using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class MissionGoal
{
    public long MissionGoalId { get; set; }

    public long? MissionId { get; set; }

    public string? GoalObjective { get; set; }

    public int? GoalValue { get; set; }

    public int? GoalAchieved { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public virtual Mission? Mission { get; set; }
}
