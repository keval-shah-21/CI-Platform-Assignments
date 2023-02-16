using System.ComponentModel.DataAnnotations;
namespace NTierDemoEntity.ViewModels;

public class EmployeeModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
