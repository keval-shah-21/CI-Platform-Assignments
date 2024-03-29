﻿using CI_Platform.Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels
{
    public class ProfileVM
    {
        public long UserId { get; set; }

        [Required]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Invalid First Name.")]
        [MaxLength(16)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Invalid Last Name.")]
        [MaxLength(16)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(10, ErrorMessage = "Phone number should be of 10 digits only.", MinimumLength = 10)]
        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string? Avatar { get; set; }

        [Display(Name = "Why I Volunteer")]
        public string? WhyIVolunteer { get; set; }

        [Display(Name = "Employee Id")]
        [StringLength(16)]
        public string? EmployeeId { get; set; }

        public string? Department { get; set; }

        [Required]
        [Display(Name = "My Profile")]
        public string? ProfileText { get; set; }

        [Display(Name = "LinkedIn URL")]
        [Url]
        public string? LinkedInUrl { get; set; }

        public string? Title { get; set; }

        [Required]
        public Availability? Availability { get; set; }

        [Required]
        [Display(Name ="City")]
        public int? CityId { get; set; }

        [Display(Name ="City")]
        public string? CityName { get; set; }

        [Required]
        [Display(Name ="Country")]
        public short? CountryId { get; set; }

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        public List<UserSkillVM>? UserSkillVMs { get; set; } = new List<UserSkillVM>();
        public List<CityVM> cityVMs { get; set; } = new();
        public List<CountryVM> countryVMs { get; set; } = new();
        public List<SkillVM> skillVMs { get; set; } = new();
    }
}
