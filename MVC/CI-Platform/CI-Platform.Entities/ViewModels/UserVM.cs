using System.ComponentModel.DataAnnotations;
namespace CI_Platform.Entities.ViewModels;

public class UserVM{
    public long UserId {get; set;}

    [Required]
    [Display(Name ="First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name ="Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Display(Name ="Email Address")]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage ="Password length should be between 8 to 255")]
    public string Password {get; set;} = string.Empty;

    [Required]
    [Display(Name ="Phone Number")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Phone Number.")]
    [StringLength(10, ErrorMessage ="Phone number should be of 10 digits only.", MinimumLength =10)]
    public string PhoneNumber {get; set;} = string.Empty;
    public string? Avatar {get; set;}
}