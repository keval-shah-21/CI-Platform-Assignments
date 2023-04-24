using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.Constants
{
    public enum Availability
    {
        [Display(Name = "Daily")]
        DAILY = 1,
        [Display(Name = "Weekly")]
        WEEKLY = 2,
        [Display(Name = "Week-end")]
        WEEK_END = 3,
        [Display(Name = "Monthly")]
        MONTHLY = 4
    }
}
