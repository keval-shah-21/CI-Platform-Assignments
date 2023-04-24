using Microsoft.EntityFrameworkCore.Storage;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IUnitOfWork{
    IUserRepository User{get;}
    IAdminRepository Admin{get;}
    IResetPasswordRepository ResetPassword {get;}
    
    IVerifyEmailRepository VerifyEmail {get;}
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

    ICommentRepository Comment { get; }

    IMissionDocumentRepository MissionDocument { get; }

    IMissionInviteRepository MissionInvite { get; }

    IStoryRepository Story { get; }
    IStoryInviteRepository StoryInvite { get; }
    IStoryMediaRepository StoryMedia { get; }

    IUserSkillRepository UserSkill { get; }

    IContactRepository Contact { get; }

    IMissionTimesheetRepository MissionTimesheet { get; }

    ICmsPageRepository CmsPage { get; }

    IBannerRepository Banner { get; }
    void Save();
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}