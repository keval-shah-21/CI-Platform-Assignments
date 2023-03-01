using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class ResetPassword
{
    public string Email { get; set; } = null!;

    public string Toekn { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}
