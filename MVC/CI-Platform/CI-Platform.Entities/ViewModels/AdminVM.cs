namespace CI_Platform.Entities.ViewModels;

public class AdminVM
{
    public byte AdminId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Avatar { get; set; }
}