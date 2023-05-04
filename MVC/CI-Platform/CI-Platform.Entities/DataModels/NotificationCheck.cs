using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class NotificationCheck
{
    public long UserId { get; set; }

    public DateTimeOffset LastCheck { get; set; }

    public virtual User User { get; set; } = null!;
}
