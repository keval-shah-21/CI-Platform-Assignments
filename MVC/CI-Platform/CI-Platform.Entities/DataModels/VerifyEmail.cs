using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class VerifyEmail
{
    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;
}
