﻿using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class UserAdminVM
{
    public long UserId { get; set; }

    [Required]
    [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Invalid First Name.")]
    [MaxLength(16)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Invalid Last Name.")]
    [MaxLength(16)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Email Address")]
    [MaxLength(128)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password length should be between 8 to 255")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Phone Number")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Phone Number.")]
    [StringLength(10, ErrorMessage = "Phone number should be of 10 digits only.", MinimumLength = 10)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Profile Text")]
    public string? ProfileText { get; set; }

    [Required]
    public int? CityId { get; set; }
    [Required]
    public short? CountryId { get; set; }

    public string? Avatar { get; set; }
    public string? EmployeeId { get; set; }

    public string? Department { get; set; }
    public bool? Status { get; set; }

    public List<CityVM> cityVMs { get; set; }
    public List<CountryVM> countryVMs { get; set; }
}
