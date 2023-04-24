using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.Constants
{
    public enum MissionStatus
    {
        [Display(Name ="Finished")]
        FINISHED,

        [Display(Name = "Ongoing")]
        ONGOING
    }
}
