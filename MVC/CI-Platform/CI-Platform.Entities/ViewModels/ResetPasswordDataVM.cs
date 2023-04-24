using System.ComponentModel.DataAnnotations;
namespace CI_Platform.Entities.ViewModels;

public class ResetPasswordDataVM : ResetPasswordVM
{
    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage ="Password length should be between 8 to 255")]
    public string Password {get; set;} = string.Empty;

    [Required]
    [Display(Name ="Confirm Password")]
    public string ConfirmPassword {get; set;} = string.Empty;

    public IEnumerable<BannerVM>? bannerVMs {get; set;}
}
