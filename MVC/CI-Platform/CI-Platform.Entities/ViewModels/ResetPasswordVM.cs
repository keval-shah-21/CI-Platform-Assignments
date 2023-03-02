namespace CI_Platform.Entities.ViewModels;

public class ResetPasswordVM
{
    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;
}
