using System.ComponentModel.DataAnnotations;
namespace CI_Platform.Entities.ViewModels;

public class LoginVM
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email {get; set;} = string.Empty;

    [Required]
    [MinLength(8, ErrorMessage ="Password should be of minimum 8 digits.")]
    public string Password {get; set;} = string.Empty;
}