using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.Constants
{
    public enum MissionType
    {   
        [Display(Name = "Time")]
        TIME,
        [Display(Name = "Goal")]
        GOAL
    }
}
