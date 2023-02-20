using System.ComponentModel.DataAnnotations;
namespace NTierDemo.Models.ViewModels;

public class EmployeeModel
{
    [Key]
    public int Id {get; set;}

    [Required(ErrorMessage = "First name is required field")]
    public string FirstName {get; set;} = string.Empty;

    public string? Role {get; set;} = string.Empty;

}
