﻿using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class Contact
{
    public long ContactId { get; set; }

    public string Subject { get; set; } = null!;

    public string? Message { get; set; }

    public bool? Status { get; set; }

    public long? UserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public string? Reply { get; set; }

    public virtual User? User { get; set; }
}
