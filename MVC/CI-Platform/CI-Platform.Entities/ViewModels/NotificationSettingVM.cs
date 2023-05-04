using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.ViewModels;

public class NotificationSettingVM
{
    public long UserId { get; set; }

    [Display(Name = "Recommend Mission")]
    public bool RecommendMission { get; set; }

    [Display(Name = "Recommend Story")]
    public bool RecommendStory { get; set; }

    [Display(Name = "Volunteering Hour")]
    public bool VolunteeringHour { get; set; }

    [Display(Name = "Volunteering Goal")]
    public bool VolunteeringGoal { get; set; }

    [Display(Name = "My Comment")]
    public bool Comment { get; set; }

    [Display(Name = "My Story")]
    public bool MyStory { get; set; }

    [Display(Name = "New Mission")]
    public bool NewMission { get; set; }

    [Display(Name = "New Message")]
    public bool NewMessage { get; set; }

    [Display(Name = "Mission Application")]
    public bool MissionApplication { get; set; }

    public bool News { get; set; }

    public bool Email { get; set; }

}
