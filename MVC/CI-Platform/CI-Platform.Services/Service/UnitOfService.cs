using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

namespace CI_Platform.Services.Service;

public class UnitOfService : IUnitOfService
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfService(IUnitOfWork unitOfWork, IEmailService emailService)
    {   
        _unitOfWork = unitOfWork;
        User = new UserService(_unitOfWork, emailService);
        ResetPassword = new ResetPasswordService(_unitOfWork);
        Mission = new MissionService(_unitOfWork);
        City = new CityService(_unitOfWork);
        Country = new CountryService(_unitOfWork);
        MissionApplication = new MissionApplicationService(_unitOfWork);
        FavouriteMission = new FavouriteMissionService(_unitOfWork);
        MissionGoal = new MissionGoalService(_unitOfWork);
        MissionMedia = new MissionMediaService(_unitOfWork);
        MissionSkill = new MissionSkillService(_unitOfWork);
        MissionTheme = new MissionThemeService(_unitOfWork);
        MissionRating = new MissionRatingService(_unitOfWork);
        Skill = new SkillService(_unitOfWork);
        Comment = new CommentService(_unitOfWork);
        MissionInvite = new MissionInviteService(_unitOfWork, emailService);
        StoryInvite = new StoryInviteService(_unitOfWork);
        Story = new StoryService(_unitOfWork);
    }

    public IUserService User{get; private set;}
    public IResetPasswordService ResetPassword{get; private set;}
    public IMissionService Mission {get; private set;}

    public ICityService City{get; private set;}

    public ICountryService Country{get; private set;}

    public IFavouriteMissionService FavouriteMission{get; private set;}

    public IMissionApplicationService MissionApplication{get; private set;}

    public IMissionGoalService MissionGoal{get; private set;}

    public IMissionMediaService MissionMedia{get; private set;}

    public IMissionRatingService MissionRating{get; private set;}

    public IMissionSkillService MissionSkill{get; private set;}

    public IMissionThemeService MissionTheme{get; private set;}

    public ISkillService Skill {get; private set;}

    public ICommentService Comment {get; private set;}  

    public IMissionInviteService MissionInvite{get; private set;}

    public IStoryInviteService StoryInvite{get; private set;}

    public IStoryService Story {get; private set;}

    public void Save(){
        _unitOfWork.Save();
    }
}
