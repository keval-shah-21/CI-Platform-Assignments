using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace CI_Platform.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        User = new UserRepository(_context);
        Admin = new AdminRepository(_context);
        ResetPassword = new ResetPasswordRepository(_context);
        Mission = new MissionRepository(_context);
        City = new CityRepository(_context);
        Country = new CountryRepository(_context);
        MissionTheme = new MissionThemeRepository(_context);
        MissionSkill = new MissionSkillRepository(_context);
        MissionRating = new MissionRatingRepository(_context);
        MissionMedia = new MissionMediaRepository(_context);
        MissionGoal = new MissionGoalRepository(_context);
        FavouriteMission = new FavouriteMissionRepository(_context);
        MissionApplication = new MissionApplicationRepository(_context);
        Skill = new SkillRepository(_context);
        Comment = new CommentRepository(_context);
        MissionDocument = new MissionDocumentRepository(_context);
        MissionInvite = new MissionInviteRepository(_context);
        StoryInvite = new StoryInviteRepository(_context);
        Story = new StoryRepository(_context);
        StoryMedia = new StoryMediaRepository(_context);
        UserSkill = new UserSkillRepository(_context);
        Contact = new ContactRepository(_context);
        MissionTimesheet = new MissionTimesheetRepository(_context);
        CmsPage = new CmsPageRepository(_context);
        Banner = new BannerRepository(_context);
        VerifyEmail = new VerifyEmailRepository(_context);
        Notification = new NotificationRepository(_context);
        NotificationCheck = new NotificationCheckRepository(_context);
        NotificationSetting = new NotificationSettingRepository(_context);
        UserNotification = new UserNotificationRepository(_context);
    }
    public IUserRepository User{get; private set;}

    public IAdminRepository Admin { get; private set;}

    public IVerifyEmailRepository VerifyEmail { get; private set; }

    public IResetPasswordRepository ResetPassword{get; private set;}

    public IMissionRepository Mission{get; private set;}

    public ICityRepository City{get; private set;}

    public ICountryRepository Country{get; private set;}

    public IFavouriteMissionRepository FavouriteMission{get; private set;}

    public IMissionApplicationRepository MissionApplication{get; private set;}

    public IMissionGoalRepository MissionGoal{get; private set;}

    public IMissionMediaRepository MissionMedia{get; private set;}

    public IMissionRatingRepository MissionRating{get; private set;}

    public IMissionSkillRepository MissionSkill{get; private set;}

    public IMissionThemeRepository MissionTheme{get; private set;}

    public ISkillRepository Skill {get; private set;}

    public ICommentRepository Comment { get; private set; }

    public IMissionDocumentRepository MissionDocument{get; private set;}

    public IMissionInviteRepository MissionInvite { get; private set; }

    public IStoryRepository Story {get; private set;}
    public IStoryInviteRepository StoryInvite { get; private set; }
    public IStoryMediaRepository StoryMedia { get; private set; }
    public IUserSkillRepository UserSkill { get; private set; }

    public IContactRepository Contact { get; private set; }

    public IMissionTimesheetRepository MissionTimesheet { get; private set;}

    public ICmsPageRepository CmsPage { get; private set; }

    public IBannerRepository Banner { get; private set; }   

    public INotificationSettingRepository NotificationSetting { get; private set; }
    public INotificationRepository Notification { get; private set; }
    public INotificationCheckRepository NotificationCheck { get; private set; }
    public IUserNotificationRepository UserNotification { get; private set; }
    public void Save()
    {
        _context.SaveChanges();
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        return await _context.Database.BeginTransactionAsync();
    }
}