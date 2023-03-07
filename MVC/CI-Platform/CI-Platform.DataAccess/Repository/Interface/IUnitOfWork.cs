namespace CI_Platform.DataAccess.Repository.Interface;

public interface IUnitOfWork{
    IUserRepository User{get;}

    IResetPasswordRepository ResetPassword {get;}

    ICityRepository City { get; }

    ICountryRepository Country { get; }

    IFavouriteMissionRepository FavouriteMission { get; }

    IMissionApplicationRepository MissionApplication { get; }

    IMissionGoalRepository MissionGoal { get; }

    IMissionMediaRepository MissionMedia { get; }

    IMissionRatingRepository MissionRating { get; }

    IMissionSkillRepository MissionSkill { get; }

    IMissionThemeRepository MissionTheme { get; }

    IMissionRepository Mission {get;}

    ISkillRepository Skill {get;}
    void Save();
}