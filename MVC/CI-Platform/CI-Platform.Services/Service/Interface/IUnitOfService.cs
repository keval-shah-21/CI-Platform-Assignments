namespace CI_Platform.Services.Service.Interface;

public interface IUnitOfService
{
    IUserService User{get;}

    IResetPasswordService ResetPassword{get;}

    IBannerService Banner{get;}

    IMissionService Mission{get;}

    ICityService City{get;}

    ICountryService Country{get;}

    IFavouriteMissionService FavouriteMission{get;}

    IMissionApplicationService MissionApplication{get;}

    IMissionGoalService MissionGoal{get;}

    IMissionMediaService MissionMedia {get;}
    IMissionDocumentService MissionDocument {get;}
    IMissionRatingService MissionRating{get;}

    IMissionSkillService MissionSkill {get;}

    IMissionThemeService MissionTheme {get;}

    ISkillService Skill{get;}

    ICommentService Comment { get; }

    IMissionInviteService MissionInvite { get; }

    IStoryInviteService StoryInvite { get; } 
    IStoryService Story { get; }

    IStoryMediaService StoryMedia { get; }

    IUserSkillService UserSkill { get; }

    IContactService Contact { get; }

    IMissionTimesheetService MissionTimesheet { get; }

    ICmsPageService CmsPage { get; }

    IUserNotificationService UserNotification { get; }
    INotificationSettingService NotificationSetting { get; }
    INotificationService Notification { get; }
    void Save();
    Task SaveAsync();
}