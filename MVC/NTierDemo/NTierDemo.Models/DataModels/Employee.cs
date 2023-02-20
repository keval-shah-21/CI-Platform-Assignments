using System;
using System.Collections.Generic;

namespace NTierDemo.Models.DataModels;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? Role { get; set; }
}
