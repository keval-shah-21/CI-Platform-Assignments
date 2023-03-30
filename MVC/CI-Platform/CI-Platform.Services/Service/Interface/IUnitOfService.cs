namespace CI_Platform.Services.Service.Interface;

public interface IUnitOfService
{
    IUserService User{get;}

    IResetPasswordService ResetPassword{get;}

    IMissionService Mission{get;}

    ICityService City{get;}

    ICountryService Country{get;}

    IFavouriteMissionService FavouriteMission{get;}

    IMissionApplicationService MissionApplication{get;}

    IMissionGoalService MissionGoal{get;}

    IMissionMediaService MissionMedia {get;}

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

    void Save();
}