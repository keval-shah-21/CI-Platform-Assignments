using System.ComponentModel.DataAnnotations;
namespace CI_Platform.Entities.ViewModels;

public class LoginVM
{
    [Required]
    [EmailAddress(ErrorMessage ="Invalid email address.")]
    [Display(Name = "Email Address")]
    public string Email {get; set;} = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage ="Password length should be between 8 to 255")]
    public string Password {get; set;} = string.Empty;

    public IEnumerable<BannerVM>? bannerVMs { get; set; }

    public string? ReturnURL { get; set; }
}